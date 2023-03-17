using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    class UserService
    {
        public User GetById(int id)
        {
            DataBaseContext context = new DataBaseContext();
            List<User> userList = context.Users.ToList();
            foreach (User user in userList.ToList())
            {
                if (user.id == id)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
