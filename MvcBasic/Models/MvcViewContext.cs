using System.Data.Entity;

namespace MvcBasic.Models
{
    public class MvcViewContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}