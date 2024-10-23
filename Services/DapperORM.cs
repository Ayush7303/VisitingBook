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

        public static IEnumerable<T> ReturnList(string sqlquery, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(sqlquery, param);
            }
        }

        public static async Task<int> AddOrUpdateAsync(string sqlquery, T modelClass, DynamicParameters dynamicParameters = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                await sqlCon.OpenAsync();
                if (modelClass != null)
                {
                    return await sqlCon.ExecuteAsync(sqlquery, modelClass);
                }
                else
                {
                    return await sqlCon.ExecuteAsync(sqlquery, dynamicParameters);
                }
            }
        }
       public static Dictionary<string, int> ReturnCount(string sqlquery, DynamicParameters param = null)
{
    using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
    {
        sqlCon.Open();
        var result = sqlCon.Query<(string Name, int Count)>(sqlquery, param).ToList();

        // Create a dictionary to store the results
        var nameCountDictionary = new Dictionary<string, int>();
        foreach (var item in result)
        {
            nameCountDictionary[item.Name] = item.Count; // Use Name as the key
        }
        return nameCountDictionary;
    }
}
    }
}
