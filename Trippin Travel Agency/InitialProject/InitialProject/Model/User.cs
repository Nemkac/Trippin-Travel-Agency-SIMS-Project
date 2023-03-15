using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitialProject.Model
{

    [Table("Users")]
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string role { get; set; }

        public User() { }

        public User(string username, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
        }
    }
}
