using InitialProject.Context;
using InitialProject.Model;
using Microsoft.VisualBasic;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class UserRepository
    {
        public User GetByUsername(string username)
        {
            using (var db = new DataBaseContext()) {
                foreach (User user in db.Users)
                {
                    if (user.username == username) {
                        return user;
                    }
                }
            return null;
            }
        }
        public List<User> GetAllUsers() 
        {
            using (var db = new DataBaseContext())
            { 
                return db.Users.ToList();   
            }
        }
    }
}
