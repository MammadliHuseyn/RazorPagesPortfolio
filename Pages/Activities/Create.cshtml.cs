using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortfolio_MVC_CORE.Models;

namespace MyPortfolio_MVC_CORE.Pages.Activities
{
    public class CreateModel : PageModel
    {
        private readonly MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext _context;

        public CreateModel(MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext context)
        {
            _context = context;

        }

        public IActionResult OnGet()
        {
        ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Activity Activity { get; set; }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Activities.Add(Activity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
