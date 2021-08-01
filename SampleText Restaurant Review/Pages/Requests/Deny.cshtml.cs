using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.Requests
{
    public class DenyModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public DenyModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RestaurantRequests RestaurantRequests { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RestaurantRequests = await _context.RestaurantRequests.FirstOrDefaultAsync(m => m.ID == id);

            if (RestaurantRequests == null)
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

            RestaurantRequests = await _context.RestaurantRequests.FindAsync(id);

            if (RestaurantRequests != null)
            {
                _context.RestaurantRequests.Remove(RestaurantRequests);
                if (await _context.SaveChangesAsync() > 0)
                {
                    // Create an auditrecord object
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Denied Restaurant";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.RestaurantName = RestaurantRequests.Name;
                    //auditrecord.ReviewID = Review.ID;
                    var userID = User.Identity.Name.ToString();
                    auditrecord.FullName = userID;

                    _context.AuditRecord.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
