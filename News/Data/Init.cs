using Microsoft.EntityFrameworkCore;

namespace News.Data
{
    public static class Init
    {
        public static async Task InitAsync(IServiceProvider serviceProvider)
        {
            var options =
                serviceProvider.GetRequiredService<DbContextOptions<NewsContext>>();
            var context = new NewsContext(options);
            await context.Database.EnsureCreatedAsync();
            
            if (context.News.Any())
                return;
            
            for (var i = 1; i <= 4; i++)
            {
                var news = new Entities.News
                {
                    Title = "News title " + i,
                    Content = "News content " + i,
                    Date = DateTime.Now.AddDays(-i),
                };

                context.News.Add(news);
            }
            
            await context.SaveChangesAsync();

        }

        
    }
}
