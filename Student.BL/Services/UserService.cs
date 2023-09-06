using Students.BL.Interface;
using Students.DAL.Interfaces;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.BL.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository IuserRepository;

        public UserService(IUserRepository userRepository)
        {
            IuserRepository = userRepository;
        }
        public bool AddUser(User user)
        {

            IuserRepository.AddUser(user);
            return true;

        }
        public User GetUserByUsername(string username)
        {
            return IuserRepository.GetUserByUsername(username);
        }

        public bool UserExist(string username)
        {
            bool check = IuserRepository.UserExist(username);
            if (check == true) return true;
            else return false;
        }
        public IEnumerable<User> GetUsers()
        {
            return IuserRepository.GetAll();

        }
        public User? GetUserByID(int id)
        {
            return IuserRepository.FindById(id);
        }
        public bool PutUser(int id, User user)
        {
            User users = GetUserByID(id);
            if (users != null)
            {
                user.UserId = id;
                IuserRepository.Update(user);
                return true;
            }
            else
            {
                AddUser(user);
                return false;
            }

        }
        public bool DeleteUser()
        {
            IuserRepository.DeleteAll();
            return true;
        }
        public bool DeleteUserByID(int id)
        {
            var user = IuserRepository.FindById(id);
            if (user != null)
            {
                IuserRepository.Delete(user);
                return true;
            }
            return false;
        }   
    }
}
