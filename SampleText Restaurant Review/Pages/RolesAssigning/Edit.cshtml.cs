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
        private readonly RoleManager<Roles> _roleManager;

        public EditModel(RoleManager<Roles> roleManager)
        {
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

            IdentityResult roleRuslt = await _roleManager.UpdateAsync(appRole);

            if (roleRuslt.Succeeded)
            {
                return RedirectToPage("./Index");

            }

            return RedirectToPage("./Index");
        }
    }
}
