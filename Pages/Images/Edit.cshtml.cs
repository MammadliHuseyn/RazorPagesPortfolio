using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPortfolio_MVC_CORE.Models;

namespace MyPortfolio_MVC_CORE.Pages.Images
{
    public class EditModel : PageModel
    {
        private readonly MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext _context;

        public EditModel(MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext context)
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
           ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MainImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainImageExists(MainImage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MainImageExists(int id)
        {
            return _context.MainImages.Any(e => e.Id == id);
        }
    }
}
