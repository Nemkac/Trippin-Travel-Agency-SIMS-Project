using InitialProject.Context;
using InitialProject.Model;

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
                    if (user.Username == username) {
                        return user;
                    }
                }
            return null;
            }
        }
    }
}
