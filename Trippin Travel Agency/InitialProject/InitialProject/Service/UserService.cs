using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class UserService
    {
        public User GetById(int id)
        {
            using DataBaseContext context = new DataBaseContext();
            return context.Users.SingleOrDefault(u => u.id == id);
        }
    }
}
