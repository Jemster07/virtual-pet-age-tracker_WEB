using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vpat.DAO;
using Vpat.Models;
using Vpat.Security;
namespace Vpat.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;

        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, IUserDao _userDao)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            userDao = _userDao;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = Unauthorized(new { message = "Username or password is incorrect" });

            // Get the user by username
            User user = userDao.GetUserByUsername(userParam.Username);

            if (user != null)
            {
                if (user.IsHidden)
                {
                    result = Unauthorized(new { message = "User account has been deactivated. Please contact support for assistance." });
                }
                else
                {
                    // If we found a user and the password hash matches
                    if (passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
                    {
                        // Create an authentication token
                        string token = tokenGenerator.GenerateToken(user.UserId, user.Username, user.Role);

                        // Create a ReturnUser object to return to the client
                        LoginResponse retUser = new LoginResponse() { User = user, Token = token };

                        // Switch to 200 OK
                        result = Ok(retUser);
                    }
                }
            }

            return result;
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public IActionResult Register(RegisterUser userParam)
        {
            IActionResult result;

            User existingUsername = userDao.GetUserByUsername(userParam.Username);
            if (existingUsername != null)
            {
                return Conflict(new { message = "Username already taken. Please choose a different username." });
            }

            User existingEmail = userDao.GetUserByEmail(userParam.Email);
            if (existingEmail != null)
            {
                return Conflict(new { message = "Email address already associated with an account. Please login or use another email address." });
            }

            User user = userDao.AddUser(userParam.Username, userParam.Email, userParam.Password, userParam.Role);
            if (user != null)
            {
                result = Created(user.Username, null); //values aren't read on client
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and user was not created." });
            }

            return result;
        }

        [HttpDelete("/deactivate/{username}")]
        public ActionResult<bool> DeactivateUser(string username)
        {
            try
            {
                bool userDeleted = userDao.DeactivateUser(username);

                if (userDeleted)
                {
                    return Ok(userDeleted);
                }
                else
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "User account was not deleted!");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize(Roles ="admin")]
        [HttpDelete("/delete/users")]
        public ActionResult<bool> DeleteUsers()
        {
            try
            {
                bool userDeleted = userDao.DeleteUsers();

                if (userDeleted)
                {
                    return Ok(userDeleted);
                }
                else
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "User accounts were not deleted!");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}