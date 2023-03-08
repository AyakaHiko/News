using System;
using System.Collections.Generic;
using System.Linq;
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
        
        [NonAction]
        public async Task<PartialViewResult> OnPostCreateCommentAsync(Comment comment)
        {
            if(comment != null)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }

            return new PartialViewResult()
            {
                ViewName = "_Comment",
                ViewData = new ViewDataDictionary<Comment>(ViewData, comment)
            };
        }
    }
    

}
