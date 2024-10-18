using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace VisitingBook.Services
{
    public static class DapperORM<T> where T : class
    {
        private static readonly string ConnectionString;

        static DapperORM()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Use the correct connection string from the configuration
            ConnectionString = config.GetConnectionString("ToolConnectionString");
        }

        public static IEnumerable<T> ReturnList(string sqlquery, DynamicParameters param)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                // Pass the parameters to the Query method
                return sqlCon.Query<T>(sqlquery, param);
            }
        }

        public static int AddOrUpdate(string sqlquery, T modelClass, DynamicParameters dynamicParameters = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                if (modelClass != null)
                {
                    return sqlCon.Execute(sqlquery, modelClass);
                }
                else
                {
                    return sqlCon.Execute(sqlquery, dynamicParameters);
                }
            }
        }
    }
}
