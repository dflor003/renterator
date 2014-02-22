//using System.Linq;
//using NUnit.Framework;
//using Renterator.DataAccess.Model;
//using Renterator.Services.Tests.Helpers;

//namespace Renterator.Services.Tests.Unit
//{
//    [TestFixture]
//    public class InMemoryAccessorTests
//    {
//        [Test]
//        public void GetAll_ShouldRetrieveAllCommitted()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            accessor.Committed.Add(new User { Id = 1, FirstName = "Bob", LastName = "Builder" });
//            accessor.Committed.Add(new User { Id = 2, FirstName = "Bill", LastName = "Billington" });
//            accessor.Committed.Add(new User { Id = 3, FirstName = "Joe", LastName = "Schmoe" });

//            // Should return all
//            IQueryable<User> allUsers = accessor.GetAll<User>();
//            Assert.IsNotEmpty(allUsers);
//            Assert.AreEqual(1, allUsers.ElementAt(0).Id);
//            Assert.AreEqual(2, allUsers.ElementAt(1).Id);
//            Assert.AreEqual(3, allUsers.ElementAt(2).Id);
//        }

//        [Test]
//        public void GetById_ShouldFindItemWithMatchingId()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            accessor.Committed.Add(new User { Id = 1 });
//            accessor.Committed.Add(new User { Id = 2 });
//            accessor.Committed.Add(new User { Id = 3 });

//            // Should return user with id 2
//            User user2 = accessor.GetById<User>(2);
//            Assert.IsNotNull(user2);
//            Assert.AreEqual(2, user2.Id);
//        }

//        [Test]
//        public void Find_ShouldGetByCriteria()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            accessor.Committed.Add(new User { Id = 1, FirstName = "Bob", LastName = "Builder" });
//            accessor.Committed.Add(new User { Id = 2, FirstName = "Bill", LastName = "Billington" });
//            accessor.Committed.Add(new User { Id = 3, FirstName = "Joe", LastName = "Schmoe" });

//            // Should return all
//            User user = accessor.Find<User>(x => x.LastName == "Schmoe").FirstOrDefault();
//            Assert.IsNotNull(user);
//            Assert.AreEqual("Joe", user.FirstName);
//        }

//        [Test]
//        public void Delete_ShouldRemoveFromSetAfterSave()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            accessor.Committed.Add(new User { Id = 1, FirstName = "Bob", LastName = "Builder" });

//            // Get it and delete it
//            Assert.AreEqual(1, accessor.Committed.Count);
//            User user = accessor.GetById<User>(1);
//            accessor.Delete(user);
//            accessor.SaveChanges();
//            Assert.AreEqual(0, accessor.Committed.Count);
//        }

//        [Test]
//        public void Create_WithNoAssociations_ShouldAddToCommittedAfterSave()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            accessor.Committed.Add(new User { Id = 1, FirstName = "Bob", LastName = "Builder" });
//            accessor.Committed.Add(new User { Id = 2, FirstName = "Chuck", LastName = "Norris" });

//            // Add it, should not add it yet
//            User newUser = new User { FirstName = "Bill", LastName = "Nye" };
//            accessor.Create(newUser);
//            Assert.AreEqual(2, accessor.Committed.Count);

//            // Save, should commit and set id
//            int numChanges = accessor.SaveChanges();
//            Assert.AreEqual(1, numChanges);
//            Assert.AreEqual(3, accessor.Committed.Count);
//            Assert.AreEqual(3, newUser.Id);
//        }

//        [Test]
//        public void Create_WithAssociations_ShouldAddToCommittedAfterSaveAndAddAssociations()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            User newUser = new User
//            {
//                FirstName = "Bob",
//                LastName = "Builder",
//                Websites =
//                {
//                    new Website
//                    {
//                        WebsiteName = "Bob's Construction"
//                    }
//                }
//            };

//            // Add it
//            accessor.Create(newUser);
//            int numChanges = accessor.SaveChanges();

//            // Should commit and set id
//            Assert.AreEqual(2, numChanges);
//            Assert.AreEqual(2, accessor.Committed.Count);

//            User user = accessor.GetById<User>(1);
//            Website website = accessor.GetById<Website>(1);
//            Assert.IsNotNull(user);
//            Assert.IsNotNull(website);
//        }

//        [Test]
//        public void Update_WithNoAssociations_ShouldUpdateExistingEntity()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            User existing = new User { Id = 10, FirstName = "Bob", LastName = "The Builder", IsActive = true };
//            accessor.Committed.Add(existing);

//            // Update with new info
//            User updated = new User { Id = 10, FirstName = "Bob", LastName = "The Builder", IsActive = false };
//            accessor.Update(updated);
//            int numChanges = accessor.SaveChanges();

//            // Should be updated
//            Assert.AreEqual(1, numChanges);
//            Assert.AreEqual(false, existing.IsActive);
//            Assert.AreNotSame(updated, existing);
//        }

//        [Test]
//        public void Update_WithExistingAssociation_ShouldUpdateExistingEntityAndChild()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            User existingUser = new User
//            {
//                Id = 10, 
//                FirstName = "Bob", 
//                LastName = "The Builder"
//            };
//            Website existingWebsite = new Website { Id = 42, WebsiteName = "Bob's construction" };
//            accessor.Committed.Add(existingUser);
//            accessor.Committed.Add(existingWebsite);

//            // Update with new info
//            User updated = new User
//            {
//                Id = 10,
//                FirstName = "Bob",
//                LastName = "The Builder Updated",
//                Websites =
//                {
//                    new Website
//                    {
//                        Id = 42,
//                        WebsiteName = "Bob's Construction Updated"
//                    }
//                }
//            };
//            accessor.Update(updated);
//            int numChanges = accessor.SaveChanges();

//            // Should be updated
//            Assert.AreEqual(2, numChanges);
//            Assert.AreEqual(2, accessor.Committed.Count);
//            Assert.AreEqual("The Builder Updated", existingUser.LastName);
//            Assert.AreEqual("Bob's Construction Updated", existingWebsite.WebsiteName);
//        }

//        [Test]
//        public void Update_WithNewAssociation_ShouldUpdateExistingEntityAndCreateChild()
//        {
//            // Setup data
//            InMemoryDataAccessor accessor = new InMemoryDataAccessor();
//            User existingUser = new User
//            {
//                Id = 10,
//                FirstName = "Bob",
//                LastName = "The Builder"
//            };
//            accessor.Committed.Add(existingUser);

//            // Update with new info
//            User updated = new User
//            {
//                Id = 10,
//                FirstName = "Bob",
//                LastName = "The Builder Updated",
//                Websites =
//                {
//                    new Website
//                    {
//                        WebsiteName = "Bob's Construction New"
//                    }
//                }
//            };
//            accessor.Update(updated);
//            int numChanges = accessor.SaveChanges();

//            // Should be updated
//            Assert.AreEqual(2, numChanges);
//            Assert.AreEqual(2, accessor.Committed.Count);
//            Assert.AreEqual("The Builder Updated", existingUser.LastName);

//            Website website = accessor.GetById<Website>(1);
//            Assert.IsNotNull(website);
//            Assert.AreEqual("Bob's Construction New", website.WebsiteName);
//        }
//    }
//}
