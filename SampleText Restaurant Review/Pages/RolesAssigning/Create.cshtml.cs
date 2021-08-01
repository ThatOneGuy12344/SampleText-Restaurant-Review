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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SampleText_Restaurant_Review.Pages.RolesAssigning
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;
        private readonly RoleManager<Roles> _roleManager;

        public CreateModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context, RoleManager<Roles> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
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

            Roles.DateCreated = DateTime.UtcNow;
            IdentityResult roleResult = await _roleManager.CreateAsync(Roles);

            if (roleResult.Succeeded)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Created Role";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.RoleName = Roles.Name;
                var userID = User.Identity.Name.ToString();
                auditrecord.FullName = userID;
                _context.AuditRecord.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
