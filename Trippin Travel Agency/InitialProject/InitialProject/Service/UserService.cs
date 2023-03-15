using InitialProject.Context;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    public class UserService
    {
        public List<User> TestFirstView() {
            DataBaseContext context = new DataBaseContext();

            User user2 = new User();
            user2.Username = "Zika";
            user2.Password = "admin";
            user2.Role = "Admin";
            context.Attach(user2); // ALO ATTACH + SAVECHANGES DA BI SE UPISALO U BAZU

            List<User> dataList = context.Users.ToList();
            foreach (User user in dataList.ToList())
            {
                if (user.Role == "TourGuide")
                {
                    dataList.Remove(user);
                }
            }
            context.SaveChanges();  
            return dataList;
        }
    }
}
