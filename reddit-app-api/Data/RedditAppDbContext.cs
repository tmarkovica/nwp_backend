using Microsoft.EntityFrameworkCore;

namespace reddit_app_api.Data
{
    public class RedditAppDbContext: DbContext
    {
        public RedditAppDbContext(DbContextOptions<RedditAppDbContext> options): base(options)
        {
        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
    }
}
