using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.BL.Interface
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public User? GetUserByID(int id);
        public bool UserExist(string username);
        public User GetUserByUsername(string username);
        public bool AddUser(User user);
        public bool PutUser(int id, User user);
        public bool DeleteUser();
        public bool DeleteUserByID(int id);
    }
}
