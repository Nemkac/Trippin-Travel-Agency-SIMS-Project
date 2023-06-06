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
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public int age { get; set; } 
        public bool super { get; set; }
        public bool resigned { get; set; }


        public User() { }

        public User(string username, string password, string role, string firstName, string lastName, string email, int age)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.age = age; 
            this.super = false;
            this.resigned = false;
        }
    }
}
