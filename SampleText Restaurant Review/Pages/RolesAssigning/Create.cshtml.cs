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

namespace SampleText_Restaurant_Review.Pages.RolesAssigning
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly RoleManager<Roles> _roleManager;

        public CreateModel(RoleManager<Roles> roleManager)
        {
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
            Roles.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            IdentityResult roleRuslt = await _roleManager.CreateAsync(Roles);


            return RedirectToPage("./Index");
        }
    }
}
