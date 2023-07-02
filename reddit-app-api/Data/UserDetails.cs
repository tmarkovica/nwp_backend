using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reddit_app_api.Data
{
    [Table("userdetails")]
    public class UserDetails
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
    }
}
