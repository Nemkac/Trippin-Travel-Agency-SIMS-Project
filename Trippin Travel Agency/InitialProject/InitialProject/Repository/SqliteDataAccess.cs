using Dapper;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class SqliteDataAccess
    {
        public static List<User> LoadUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<User>("select * from Users", new DynamicParameters());
                return output.ToList();
            }

        }
        public static void SaveUsers(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Users (Username,Password) values(@Username, @Password)", user);
            }
        }

        private static string LoadConnectionString(string id = "Default") 
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
