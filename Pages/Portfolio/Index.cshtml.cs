using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio_MVC_CORE.Models;
using Microsoft.EntityFrameworkCore;

namespace MyPortfolio_MVC_CORE
{
    public class PortfolioModel : PageModel
    {
        private readonly MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext _context;

        public PortfolioModel(MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext context)
        {
            _context = context;
        }

        public IList<Activity> Activities { get; set; }
        public Owner Owner { get; set; }
        public IList<MainImage> MainImg { get; set; }
        public IList<Models.Service> Services {get;set;}
        public async Task OnGetAsync()
        {
            Activities = await _context.Activities.ToListAsync();
            Owner = await _context.Owners.FirstOrDefaultAsync();
            Services = await _context.Services.ToListAsync();
            MainImg = await _context.MainImages.ToListAsync();
        }
    }
}