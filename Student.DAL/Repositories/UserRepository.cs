using Dapper;
using Students.DAL.Interfaces;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Security;

namespace Students.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public async Task<int> AddUser(User user)
        {
            User existingUser = FindByUsername(user.Username);
            if (existingUser != null)
            {
                return -1; // Username already exists
            }
            else
            {
                Add(user);
                return 1;
            }
            
        }

        public User GetUserByUsername(string username)
        {
            return FindByUsername(username);
        }

        public bool UserExist(string username)
        {
            User check = GetUserByUsername(username);
            if (check != null) return true;
            else return false;
        }
    }
}
