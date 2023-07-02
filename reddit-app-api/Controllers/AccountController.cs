using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using reddit_app_api.Data;
using reddit_app_api.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    }
}
