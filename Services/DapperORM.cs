using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
// using System.Data;

namespace VisitingBook.Services
{    
    public static class DapperORM<T> where T : class
    {
        private static readonly string ConnectionString = "";

        static DapperORM()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

#pragma warning disable CS8601 // Possible null reference assignment.
            ConnectionString = config.GetConnectionString("ToolConnectionString");
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        public static IEnumerable<T> ReturnList<T>(string sqlquery, DynamicParameters param=null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(sqlquery).ToList();
            }
        }
        public static int AddOrUpdate(string sqlquery,T ModelClass,DynamicParameters dynamicParameters)
        {
            using(SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                if(ModelClass!= null)
                    return sqlcon.Execute(sqlquery,ModelClass);
                else
                    return sqlcon.Execute(sqlquery,dynamicParameters);
            }
        }     
    }
}