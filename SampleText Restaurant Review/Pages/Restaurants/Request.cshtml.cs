using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.Requests
{
    [Authorize(Roles = "Admin, User")]
    public class RequestModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public RequestModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RestaurantRequests RestaurantRequests { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            RestaurantRequests.RequestDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RestaurantRequests.Add(RestaurantRequests);
            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Requested Restaurant";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.RestaurantName = RestaurantRequests.Name;
                //auditrecord.ReviewID = Review.ID;
                var userID = User.Identity.Name.ToString();
                auditrecord.FullName = userID;

                _context.AuditRecord.Add(auditrecord);
                await _context.SaveChangesAsync();
            }
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
