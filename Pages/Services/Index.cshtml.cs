using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio_MVC_CORE.Models;

namespace MyPortfolio_MVC_CORE
{
    public class IndexModel : PageModel
    {
        private readonly MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext _context;

        public IndexModel(MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext context)
        {
            _context = context;
        }

        public IList<Service> Service { get;set; }

        public async Task OnGetAsync()
        {
            Service = await _context.Services
                .Include(s => s.Owner).ToListAsync();
        }
    }
}
