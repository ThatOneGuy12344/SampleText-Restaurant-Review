using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public DeleteModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurant.FirstOrDefaultAsync(m => m.ID == id);

            if (Restaurant == null)
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

            Restaurant = await _context.Restaurant.FindAsync(id);

            if (Restaurant != null)
            {
                foreach (var item in _context.Reviews)
                {
                    if (item.Restaurant == Restaurant)
                    {
                        _context.Reviews.Remove(item);
                    }
                }
                _context.Restaurant.Remove(Restaurant);
                //await _context.SaveChangesAsync();
                if (await _context.SaveChangesAsync() > 0)
                {
                    // Create an auditrecord object
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Deleted Restaurant";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.RestaurantName = Restaurant.Name;
                    var userID = User.Identity.Name.ToString();
                    auditrecord.FullName = userID;

                    _context.AuditRecord.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
