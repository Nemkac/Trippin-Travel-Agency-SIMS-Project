using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class UserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            //_users = _serializer.FromCSV(FilePath);
            _users = SqliteDataAccess.LoadUsers();
        }

        public User GetByUsername(string username)
        {
            // _users = _serializer.FromCSV(FilePath);
            _users = SqliteDataAccess.LoadUsers();
            return _users.FirstOrDefault(u => u.Username == username);
        }
    }
}
