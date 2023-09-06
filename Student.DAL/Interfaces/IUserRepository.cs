using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.DAL.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        public User GetUserByUsername(string username);
        public Task<int> AddUser(User user);
        public bool UserExist(string username);
    }
}
