using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortfolio_MVC_CORE.Models;

namespace MyPortfolio_MVC_CORE.Pages.Images
{
    public class CreateModel : PageModel
    {
        private readonly MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext _context;
        private IHostingEnvironment _environment;
        public CreateModel(MyPortfolio_MVC_CORE.Models.MyPortfolioDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["ActivityId"] = new SelectList(_context.Activities, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public MainImage MainImage { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var file = Path.Combine(_environment.ContentRootPath, "wwwroot/Images/Activities", Image.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }
            MainImage.FileDirectory = $"Images/Activities/{Image.FileName}";
            _context.MainImages.Add(MainImage);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
