using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using News.Data;

namespace News.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly NewsContext _context;

        public DetailsModel(NewsContext context)
        {
            _context = context;
        }
        
      public Data.Entities.News News { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n=> n.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            else 
            {
                News = news;
            }
            return Page();
        }

        [BindProperty]
        public int NewsId { get; set; }
        [BindProperty]
        public string CommentContent { get; set; } = default!;

        public async Task<IActionResult> OnPostCreateCommentAsync()
        {
            var comment = new Data.Entities.Comment
            {
                NewsId = NewsId,
                Content = CommentContent,
                Date = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToPage("/News/Details", new { id = NewsId });
        }

        //public async Task<IActionResult> OnPostCreateCommentAsync(int newsId, string commentContent)
        //{
        //    var comment = new Data.Entities.Comment
        //    {
        //        NewsId = newsId,
        //        Content = commentContent,
        //        Date = DateTime.Now
        //    };

        //    _context.Comments.Add(comment);
        //    await _context.SaveChangesAsync();
        //    return RedirectToPage("/News/Details", new { id = NewsId });
        //}
    }
    

}
