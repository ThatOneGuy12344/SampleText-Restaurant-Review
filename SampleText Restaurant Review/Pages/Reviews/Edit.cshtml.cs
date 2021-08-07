using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.Reviews
{
    [Authorize(Roles = "Admin, User")]
    public class EditModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;

        public EditModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context)
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

            Review = await _context.Reviews.Include(item => item.Restaurant).FirstOrDefaultAsync(m => m.ID == id);
            if (Review == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //Review.ReviewTime = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_context.Reviews.Find(id).Reviewer.Equals(User.Identity.Name.ToString()) && !User.IsInRole("Admin"))
            {
                return RedirectToPage("./Index");
            }

            if (Review != null)
            {
                //Review.Reviewer = User.Identity.Name.ToString();
                //string name = (await _context.Reviews.Include(item => item.Restaurant).FirstOrDefaultAsync(m => m.ID == id)).Restaurant.Name;
                try
                {
                    _context.Attach(Review).State = EntityState.Modified;
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        // Create an auditrecord object
                        var auditrecord = new AuditRecord();
                        auditrecord.AuditActionType = "Edited Review";
                        auditrecord.DateTimeStamp = DateTime.Now;
                        auditrecord.ReviewID = Review.ID;
                        var userID = User.Identity.Name.ToString();
                        auditrecord.FullName = userID;
                        _context.AuditRecord.Add(auditrecord);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(Review.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ID == id);
        }
    }
}
