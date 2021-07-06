using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.RolesAssigning
{
    public class CreateModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public CreateModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Roles Roles { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Roles.Add(Roles);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
