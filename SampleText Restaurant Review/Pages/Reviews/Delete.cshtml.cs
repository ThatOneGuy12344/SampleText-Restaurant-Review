using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public DeleteModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review = await _context.Reviews.FirstOrDefaultAsync(m => m.ID == id);

            if (Review == null)
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

            Review = await _context.Reviews.Include(item => item.Restaurant).FirstOrDefaultAsync(m => m.ID == id);

            if (Review != null)
            {
                _context.Reviews.Remove(Review);
                //await _context.SaveChangesAsync();
                if (await _context.SaveChangesAsync() > 0)
                {
                    // Create an auditrecord object
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Deleted Review";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.RestaurantName = Review.Restaurant.Name;
                    auditrecord.ReviewID = Review.ID;
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
