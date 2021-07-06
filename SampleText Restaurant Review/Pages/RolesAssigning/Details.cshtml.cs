using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.RolesAssigning
{
    public class DetailsModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public DetailsModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        public Roles Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Roles = await _context.Roles.FirstOrDefaultAsync(m => m.ID == id);

            if (Roles == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
