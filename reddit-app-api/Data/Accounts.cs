using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reddit_app_api.Data
{
    [Table("accounts")]
    public class Accounts
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
