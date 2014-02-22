using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Renterator.DataAccess.Tests.Helpers
{
    internal static class DbHelper
    {
        private const string TestConnStringName = "TestHelperConnString";
        private static readonly string TestConnString;

        static DbHelper()
        {
            TestConnString = ConfigurationManager.ConnectionStrings[TestConnStringName].ConnectionString;
        }

        public static void SetIdentitySeed(string tableName, int seed)
        {
            string sql = string.Format("DBCC CHECKIDENT ('{0}', RESEED, {1})", tableName, seed);
            ExecuteNonQuery(sql);
        }

        public static void ExecuteNonQuery(string sql)
        {
            using (SqlConnection connection = new SqlConnection(TestConnString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static T ExecuteScalar<T>(string sqlFormat, params object[] formatArgs)
        {
            string sql = string.Format(sqlFormat, formatArgs);
            using (SqlConnection connection = new SqlConnection(TestConnString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                return (T)command.ExecuteScalar();
            }
        }

        public static void ExecuteReader(string sql, Action<IDataReader> action)
        {
            using (SqlConnection connection = new SqlConnection(TestConnString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    action(reader);	
                }
            }
        }
    }
}
