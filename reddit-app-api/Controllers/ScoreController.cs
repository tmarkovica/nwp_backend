using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reddit_app_api.Data;
using reddit_app_api.Models;
using System.Diagnostics;

namespace reddit_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private RedditAppDbContext _dbContext;

        public ScoreController(RedditAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("Score/{id}")]
        public IActionResult CreateScoreProfile([FromBody] ScoreProfileRequest request, int id)
        {
            try
            {
                decimal score = request.score;
                string note = request.note;

                var result = _dbContext.Database.ExecuteSqlRaw(
                    "call proc_create_score_profile({0}, {1}, {2})",
                    id,
                    score,
                    note
                    );

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("Score/{id}")]
        public IActionResult ScoreProfilesForAccount(int id)
        {
            try
            {
                var result = _dbContext.ScoreProfile.FromSqlRaw("SELECT * FROM func_get_score_profiles({0})", id).ToList();

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("Score/{accountId}/{scoreProfileId}")]
        public IActionResult DeleteScoreProfileForAccount(int accountId, int scoreProfileId)
        {
            try
            {
                var result = _dbContext.Database.ExecuteSqlRaw("call proc_delete_score_profile({0}, {1})", accountId, scoreProfileId);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("Score/{scoreProfileId}")]
        public IActionResult UpdateScoreProfileForAccount([FromBody] ScoreProfileRequest request, int scoreProfileId)
        {
            try
            {
                string note = request.note;
                decimal score = request.score;

                Debug.WriteLine(">>>>>>> score: " + score);
                Debug.WriteLine(">>>>>>> note: " + note);

                var result = _dbContext.Database.ExecuteSqlRaw(
                    "call proc_update_score_profile({0}, {1}, {2})", 
                    scoreProfileId,
                    score, 
                    note
                    );

                Debug.WriteLine(">>>>>>> result:", result.ToString());

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
