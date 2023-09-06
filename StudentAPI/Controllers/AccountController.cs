using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Students.API.Models.Request;
using Students.BL.Interface;
using Students.Models;
using Utility.Security;

namespace Students.API.Controllers
{
    [Route("Account")]
    [ApiController]
    [AllowAnonymous] // without any token 
    public class AccountController : Controller
    {
        private readonly IUserService _IuserService;
        //Constructor
        public AccountController(IUserService IuserService)
        {
            this._IuserService = IuserService;
        }

        [Route("SignUp/")]
        [HttpPost]
        public string SignUp(SigninRequest signinRequest) //Registration
        {
            if (_IuserService.UserExist(signinRequest.Username) == true)
            {
                return "User Already Exists";
            }
            else
            {
                User newUser = new User
                {
                    Username = signinRequest.Username, // Make sure you set the username
                    Password = EncryptorDecryptorEngine.EncryptString(signinRequest.Password),
                    Email = signinRequest.Email,
                    Fullname = signinRequest.Fullname,
                    UserTypeId = 2
                    // Set other properties accordingly
                };
                _IuserService.Adapt<User>();
                _IuserService.AddUser(newUser);
                return "New User is Added Successfully";
            }
        }

        [Route("SignIn/")]
        [HttpPost]
        public string SignIn(LoginRequest loginRequest) //login
        {
            User user =  _IuserService.GetUserByUsername(loginRequest.Username);

            if (user != null) // Check if the user exists
            {
                string encryptedPassword = EncryptorDecryptorEngine.EncryptString(loginRequest.Password);

                if (encryptedPassword == user.Password)
                {
                    // Passwords match, generate and return a token
                    return JWTBuilder.Generation(user.UserId, user.Username, user.UserTypeId??2).Item1;
                }
                else
                {
                    return "Password or UserName not correct";
                }
            }
            else
            {
                return "User Not Found";
            }

        }

        //sign in 
        // it will check if user exist or not.
        //if exist then give the error
    }
}
