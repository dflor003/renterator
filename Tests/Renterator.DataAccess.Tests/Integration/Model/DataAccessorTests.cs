//using System;
//using System.Linq;
//using FiveMinuteMobile.DataAccess.Model;
//using FiveMinuteMobile.DataAccess.Tests.Helpers;
//using NUnit.Framework;
//using Renterator.Common;
//using Renterator.DataAccess.Infrastructure;
//using Renterator.DataAccess.Model;

//namespace FiveMinuteMobile.DataAccess.Tests.Integration.Model
//{
//    [TestFixture]
//    [ServiceStart("msdtc")]
//    public class DataAccessorTests
//    {
//        private const int UserIdSeed = 10000;
//        private const int MinUserId = UserIdSeed + 1;
//        private const int WebsiteIdSeed = 10000;
//        private const int MinWebsiteId = WebsiteIdSeed + 1;

//        [SetUp]
//        public void SetUp()
//        {
//            DbHelper.SetIdentitySeed("Users", UserIdSeed);
//            DbHelper.SetIdentitySeed("Websites", UserIdSeed);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            DbHelper.SetIdentitySeed("Users", 0);
//            DbHelper.SetIdentitySeed("Websites", 0);
//        }

//        [Test, Rollback]
//        public void Create_ShouldAddToDbAndReturnNewId()
//        {
//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Dummy data
//                User newUser = BuildUser();

//                // Id should not be set initially
//                Assert.AreEqual(0, newUser.Id);

//                // Create and save
//                dataAccessor.Create(newUser);
//                dataAccessor.SaveChanges();

//                // Id should be incremented from identity
//                Assert.AreEqual(MinUserId, newUser.Id);
//                Assert.AreEqual("Bob", newUser.FirstName);
//                Assert.AreEqual("TheBuilder", newUser.LastName);

//                // Should add to DB
//                DbHelper.ExecuteReader(@"
//                    SELECT Id, FirstName, LastName
//                    FROM Users
//                    WHERE Id = 10001", reader =>
//                {
//                    // Should return 1 record
//                    Assert.IsTrue(reader.Read());
//                    Assert.AreEqual(MinUserId, reader.GetInt32(0));
//                    Assert.AreEqual("Bob", reader.GetString(1));
//                    Assert.AreEqual("TheBuilder", reader.GetString(2));
//                });
//            }
//        }

//        [Test, Rollback]
//        public void Get_ShouldReturnItemThatMatchesId()
//        {
//            // Force record into DB
//            InsertRecords(BuildUser(
//                email: "DanilF@email.com",
//                firstName: "Danil",
//                lastName: "Flores"));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Get user
//                User retrievedUser = dataAccessor.GetById<User>(10001);

//                // Assert right one comes back
//                Assert.AreEqual(MinUserId, retrievedUser.Id);
//                Assert.AreEqual("DanilF@email.com", retrievedUser.Email);
//                Assert.AreEqual("Danil", retrievedUser.FirstName);
//                Assert.AreEqual("Flores", retrievedUser.LastName);
//            }
//        }

//        [Test, Rollback]
//        public void Get_ShouldReturnNull_IfNoMatchingItem()
//        {
//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Get user
//                User retrievedUser = dataAccessor.GetById<User>(1000000);

//                // Should be null
//                Assert.IsNull(retrievedUser);
//            }
//        }

//        [Test, Rollback]
//        public void GetAll_ShouldReturnAllRecords()
//        {
//            // Insert dummy records
//            InsertRecords(
//                BuildUser(email: "User1@email.com"),
//                BuildUser(email: "User2@email.com"),
//                BuildUser(email: "User3@email.com"),
//                BuildUser(email: "User4@email.com"));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Get all users
//                User[] allUsers = dataAccessor.GetAll<User>().Where(x => x.Id >= MinUserId).ToArray();

//                // Assert all records returned
//                Assert.IsNotNull(allUsers);
//                Assert.AreEqual(4, allUsers.Length);
//                Assert.AreEqual("User1@email.com", allUsers[0].Email);
//                Assert.AreEqual("User4@email.com", allUsers[3].Email);
//                Assert.AreEqual(10001, allUsers[0].Id);
//                Assert.AreEqual(10004, allUsers[3].Id);
//            }
//        }

//        [Test, Rollback]
//        public void GetAll_ShouldSupportComplexQueries()
//        {
//            // Insert dummy records
//            InsertRecords(
//                BuildUser(email: "User1@email.com", lastLoginDate: new DateTime(2012, 01, 01), firstName: "Chuck"),
//                BuildUser(email: "User2@email.com", lastLoginDate: new DateTime(2013, 01, 01), firstName: "Al"),
//                BuildUser(email: "User3@email.com", lastLoginDate: new DateTime(2010, 01, 01), firstName: "Bill"),
//                BuildUser(email: "User4@email.com", lastLoginDate: new DateTime(2009, 01, 01), firstName: "Frederick"));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Get users in complex query
//                var matchingUsersQueryable =
//                    from user in dataAccessor.Users
//                    where
//                        user.LastLoginDate.Year >= 2010 &&
//                        user.LastLoginDate.Year <= 2012
//                    orderby user.FirstName
//                    select new { user.Email, user.FirstName };
//                var matchingUsers = matchingUsersQueryable.ToArray();

//                // Assert matching records returned
//                Assert.IsNotNull(matchingUsers);
//                Assert.AreEqual(2, matchingUsers.Length);
//                Assert.AreEqual("User3@email.com", matchingUsers[0].Email);
//                Assert.AreEqual("User1@email.com", matchingUsers[1].Email);
//                Assert.AreEqual("Bill", matchingUsers[0].FirstName);
//                Assert.AreEqual("Chuck", matchingUsers[1].FirstName);
//            }
//        }

//        [Test, Rollback]
//        public void Find_ShouldFilterSet()
//        {
//            // Insert dummy records
//            InsertRecords(
//                BuildUser(email: "User1", firstName: "Chuck"),
//                BuildUser(email: "User2", firstName: "Al"),
//                BuildUser(email: "User3", firstName: "Bill"),
//                BuildUser(email: "User4", firstName: "Frederick"));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Get users matching lambda
//                var matchingUsersQueryable = dataAccessor
//                    .Find<User>(x => x.FirstName.Contains("l") && x.Id >= MinUserId)
//                    .Select(x => new { x.FirstName });
//                var matchingUsers = matchingUsersQueryable.ToArray();

//                // Assert matching records returned
//                Assert.IsNotNull(matchingUsers);
//                Assert.AreEqual(2, matchingUsers.Length);
//                Assert.AreEqual("Al", matchingUsers[0].FirstName);
//                Assert.AreEqual("Bill", matchingUsers[1].FirstName);
//            }
//        }

//        [Test, Rollback]
//        public void Update_SingleEntity_ShouldUpdateRecordInDb()
//        {
//            // Insert dummy records
//            InsertRecords(
//                BuildUser(email: "BadassUser@email.com", firstName: "Chuck", lastName: "Norris", isActive: false));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Get user
//                User chuck = dataAccessor.GetById<User>(10001);
//                Assert.AreEqual("Chuck", chuck.FirstName);
//                Assert.AreEqual("Norris", chuck.LastName);
//                Assert.AreEqual(false, chuck.IsActive);
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT 1
//                        FROM Users
//                        WHERE Email = 'BadassUser@email.com' AND IsActive = 0"));

//                // Update
//                chuck.IsActive = true;
//                dataAccessor.Update(chuck);
//                dataAccessor.SaveChanges();

//                // Assert values updated
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT 1
//                        FROM Users
//                        WHERE Email = 'BadassUser@email.com' AND IsActive = 1"));
//            }
//        }

//        [Test, Rollback]
//        public void Update_MultipleEntities_ShouldUpdateAllRecordsInDb()
//        {
//            // Insert dummy records
//            InsertRecords(
//                BuildUser(email: "User1@email.com", firstName: "Chuck", isActive: false),
//                BuildUser(email: "User2@email.com", firstName: "Al", isActive: false),
//                BuildUser(email: "User3@email.com", firstName: "Joe", isActive: false));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Initial check
//                User[] users = dataAccessor.Find<User>(x => x.Id >= MinUserId + 1).ToArray();
//                Assert.IsNotNull(users);
//                Assert.AreEqual(2, users.Length);

//                // Update items
//                users.ForEach(x => x.IsActive = true);
//                dataAccessor.Update(users[0], users[1]);
//                dataAccessor.SaveChanges();

//                // Assert values updated
//                Assert.AreEqual(2,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM Users
//                        WHERE Id >= {0} AND IsActive = 1", MinUserId));
//            }
//        }

//        [Test, Rollback]
//        public void Delete_SingleEntity_ShouldRemoveRecordFromDb()
//        {
//            InsertRecords(
//                BuildUser(email: "User@email.com", firstName: "Chuck"),
//                BuildUser(email: "User@email.com", firstName: "Al"));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Initial check
//                User[] users = dataAccessor.Find<User>(x => x.Id >= MinUserId).ToArray();
//                Assert.IsNotNull(users);
//                Assert.AreEqual(2, users.Length);

//                // Delete
//                dataAccessor.Delete(users[0]);
//                dataAccessor.SaveChanges();

//                // Assert values deleted
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM Users
//                        WHERE Id >= {0}", MinUserId));
//                users = dataAccessor.Find<User>(x => x.Id >= MinUserId).ToArray();
//                Assert.IsNotNull(users);
//                Assert.AreEqual(1, users.Length);
//            }
//        }

//        [Test, Rollback]
//        public void Delete_MultipleEntities_ShouldRemoveAllRecordsFromDb()
//        {
//            InsertRecords(
//                BuildUser(email: "User1@email.com", firstName: "Chuck"),
//                BuildUser(email: "User2@email.com", firstName: "Al"),
//                BuildUser(email: "User3@email.com", firstName: "Joe"));

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Initial check
//                User[] users = dataAccessor.Find<User>(x => x.Id >= MinUserId).ToArray();
//                Assert.IsNotNull(users);
//                Assert.AreEqual(3, users.Length);

//                // Delete
//                dataAccessor.Delete(users[1], users[2]);
//                dataAccessor.SaveChanges();

//                // Assert values deleted
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM Users
//                        WHERE Id >= {0}", MinUserId));
//                users = dataAccessor.Find<User>(x => x.Id >= MinUserId).ToArray();
//                Assert.IsNotNull(users);
//                Assert.AreEqual(1, users.Length);
//                Assert.AreEqual("Chuck", users[0].FirstName);
//            }
//        }

//        [Test, Rollback]
//        public void Delete_WithPredicate_ShouldRemoveMatchingRecords()
//        {
//            // Insert dummy records
//            // ReSharper disable RedundantArgumentDefaultValue
//            InsertRecords(
//                BuildUser(email: "User1@email.com", firstName: "Chuck", isActive: false),
//                BuildUser(email: "User2@email.com", firstName: "Al", isActive: true),
//                BuildUser(email: "User3@email.com", firstName: "Joe", isActive: false),
//                BuildUser(email: "User4@email.com", firstName: "Bob", isActive: false));
//            // ReSharper restore RedundantArgumentDefaultValue

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Initial check
//                Assert.AreEqual(4,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM Users
//                        WHERE Id >= {0}", MinUserId));

//                // Perform delete
//                dataAccessor.Delete<User>(x => x.IsActive == false);
//                dataAccessor.SaveChanges();

//                // Assert values deleted
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM Users
//                        WHERE Id >= {0}", MinUserId));
//                Assert.IsNotNull(dataAccessor.Find<User>(x => x.Email == "User2@email.com").Single());
//            }
//        }

//        [Test, Rollback]
//        public void Update_WithAssociations_ShouldAddDbEntry()
//        {
//            // Create dummy record
//            User user = BuildUser(email: "chuck@norris.com", firstName: "Chuck", lastName: "Norris");
//            Website website = new Website
//            {
//                WebsiteCode = "abcd1234",
//                WebsiteName = "Chuck's Karate",
//                IsPublished = false,
//                IsActive = false,
//                LastModifiedDate = new DateTime(2012, 01, 01)
//            };
//            InsertRecords(user);
//            InsertRecords(website);

//            using (IDataAccessor dataAccessor = new RenteratorDataAccessor())
//            {
//                // Attach entities to context
//                dataAccessor.Attach(user);
//                dataAccessor.Attach(website);

//                // Assert initial values
//                Assert.AreEqual(MinUserId, user.Id);
//                Assert.AreEqual(MinWebsiteId, website.Id);
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM Websites
//                        WHERE Id = {0}", website.Id));
//                Assert.AreEqual(0,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM WebsiteUsers
//                        WHERE UserId = {0} AND WebsiteId = {1}", user.Id, website.Id));

//                // Add association and update
//                user.Websites.Add(website);
//                dataAccessor.SaveChanges();

//                // Assert value added to association table
//                Assert.AreEqual(1,
//                    DbHelper.ExecuteScalar<int>(@"
//                        SELECT COUNT(*)
//                        FROM WebsiteUsers
//                        WHERE UserId = {0} AND WebsiteId = {1}", user.Id, website.Id));
//            }
//        }

//        #region Helpers
//        private void InsertRecords<T>(params T[] items) where T : class, new()
//        {
//            if (items == null)
//            {
//                throw new ApplicationException("Insert records, no items passed");
//            }

//            using (IDataAccessor accessor = new RenteratorDataAccessor())
//            {
//                foreach (T item in items)
//                {
//                    accessor.Create(item);
//                }

//                accessor.SaveChanges();
//            }
//        }

//        private User BuildUser(
//            int id = 0,
//            string firstName = "Bob",
//            string lastName = "TheBuilder",
//            bool isActive = true,
//            bool isAdmin = false,
//            string email = "bob@bobsbuilders.com",
//            DateTime? lastLoginDate = null,
//            string passwordHash = null)
//        {
//            return new User
//            {
//                Id = id,
//                FirstName = firstName,
//                LastName = lastName,
//                Email = email,
//                IsActive = isActive,
//                IsAdmin = isAdmin,
//                LastLoginDate = lastLoginDate ?? new DateTime(1986, 01, 17),
//                PasswordHash = passwordHash ?? DataHelper.RepeatString('X', 70)
//            };
//        }
//        #endregion
//    }
//}
