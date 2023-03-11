using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using News.Data;
using News.Data.Entities;

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
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            News = news;
            return Page();
        }

        [NonAction]
        public async Task<IActionResult> OnPostCreateCommentAsync(Comment comment)
        {
            if (_checkComment(comment.Content))
            {
                return BadRequest("Error comment");

            }
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return new PartialViewResult
            {
                ViewName = "_Comment",
                ViewData = new ViewDataDictionary<Comment>(ViewData, comment)
            };
        }

        private bool _checkComment(string content)
            => ForbiddenWords.IsForbidden(content);


        public async Task<IActionResult> OnPostDeleteCommentAsync([FromBody] int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            var newsId = comment.NewsId;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();


            return RedirectToPage("./Details", new { id = newsId });
        }

        public async Task<IActionResult> OnPostUpdateCommentAsync(int id, string content)
        {
            if (_checkComment(content))
            {
                return BadRequest("Error comment");
            }
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            
            comment.Content = content;
            await _context.SaveChangesAsync();

            return new PartialViewResult
            {
                ViewName = "_Comment",
                ViewData = new ViewDataDictionary<Comment>(ViewData, comment)
            };
        }
    }


}
