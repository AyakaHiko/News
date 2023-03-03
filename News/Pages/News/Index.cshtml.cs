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
    public class IndexModel : PageModel
    {
        private readonly NewsContext _context;

        public IndexModel(NewsContext context)
        {
            _context = context;
        }

        public IList<Data.Entities.News> News { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.News != null)
            {
                News = await _context.News.ToListAsync();
            }
        }
    }
}
