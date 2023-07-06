using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reddit_app_api.Data
{
    [Table("score_profiles")]
    public class ScoreProfile
    {
        [Key]
        public int id { get; set; }
        public decimal score { get; set; }
        public string note { get; set; }
    }
}
