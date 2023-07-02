using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reddit_app_api.Data;
using System.Diagnostics;

namespace reddit_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController: ControllerBase
    {
        private RedditAppDbContext _dbContext;

        public UserDetailsController(RedditAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var userDetails = _dbContext.UserDetails.ToList();

                if (userDetails.Count > 0)
                    return Ok(userDetails);
                else
                    return StatusCode(404, "No UserDetails found");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
