using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reddit_app_api.Data;
using reddit_app_api.Models;
using System.Diagnostics;

namespace reddit_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private RedditAppDbContext _dbContext;

        public AccountController(RedditAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("Login")] // https://localhost:44360/api/Account/Login
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                string email = request.email;
                string password = request.password;

                var result = _dbContext.Accounts.FromSqlRaw("SELECT * FROM login({0}, {1})", email, password).ToList();
                
                Debug.WriteLine(email, password);

                if (result.Count == 0)
                    return NotFound("No such user");
                else
                    return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegistrationRequest request)
        {
            try
            {
                string username = request.username;
                string email = request.email;
                string password = request.password;

                var result = _dbContext.Accounts.FromSqlRaw("SELECT * FROM register({0}, {1}, {2})", username, email, password).ToList();

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Username or Email already taken");
            }
        }

        [HttpPost("RedditAccount")]
        public IActionResult RedditAccountLogin([FromBody] RedditLoginRequest request)
        {
            try
            {
                Debug.WriteLine("id: " + request.id + " ; username: " + request.username);
                var result = _dbContext.Accounts.FromSqlRaw("SELECT * FROM func_reddit_login({0}, {1})", request.id, request.username).ToList();

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with reddit login");
            }
        }
    }
}
