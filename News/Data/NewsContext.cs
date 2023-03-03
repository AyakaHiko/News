using Microsoft.EntityFrameworkCore;

namespace News.Data
{
    public class NewsContext: DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
        }

        public DbSet<Entities.News> News => Set<Entities.News>();
    }
}
