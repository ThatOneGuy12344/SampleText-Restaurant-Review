using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleText_Restaurant_Review.Data;
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.RolesAssigning
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;
        private readonly RoleManager<Roles> _roleManager;

        public EditModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context, RoleManager<Roles> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }


        [BindProperty]
        public Roles Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Roles = await _roleManager.FindByIdAsync(id);
            

            if (Roles == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Roles appRole = await _roleManager.FindByIdAsync(Roles.Id);

            appRole.Id = Roles.Id;
            appRole.Name = Roles.Name;
            appRole.Desc = Roles.Desc;

            IdentityResult roleResult = await _roleManager.UpdateAsync(appRole);

            if (roleResult.Succeeded)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Edited Role";
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
