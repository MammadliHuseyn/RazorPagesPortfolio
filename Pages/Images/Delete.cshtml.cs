using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio_MVC_CORE.Models;

namespace MyPortfolio_MVC_CORE.Pages.Images
{
    public class DeleteModel : PageModel
    {
        private readonly MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext _context;

        public DeleteModel(MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MainImage MainImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MainImage = await _context.MainImages
                .Include(m => m.Activity).FirstOrDefaultAsync(m => m.Id == id);

            if (MainImage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MainImage = await _context.MainImages.FindAsync(id);

            if (MainImage != null)
            {
                _context.MainImages.Remove(MainImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
