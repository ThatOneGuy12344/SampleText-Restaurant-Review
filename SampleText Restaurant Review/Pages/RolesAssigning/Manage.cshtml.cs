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
using SampleText_Restaurant_Review.Models;

namespace SampleText_Restaurant_Review.Pages.RolesAssigning
{
    [Authorize(Roles = "Admin")]
    public class ManageModel : PageModel
    {
        private readonly SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Roles> _roleManager;


        public ManageModel(SampleText_Restaurant_Review.Data.SampleText_Restaurant_ReviewContext context, 
            UserManager<ApplicationUser> userManager, RoleManager<Roles> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public SelectList RolesSelectList;

        public SelectList UsersSelectList;
        public string selectedRoleName { get; set; }
        public string selectedUserName { get; set; }
        public string delRoleName { get; set; }
        public string delUserName { get; set; }
        public int userCountForRole { get; set; }
        public IList<Roles> Listroles { get; set; }

        public string ListUsersInRole(string rolename)
        {
            string strListUsersInRole = "";
            string roleid = _roleManager.Roles.SingleOrDefault(u => u.Name == rolename).Id;

            var count = _context.UserRoles.Where(u => u.RoleId == roleid).Count();
            userCountForRole = count;

            var listusers = _context.UserRoles.Where(u => u.RoleId == roleid);

            foreach (var oParam in listusers)
            {    
                var userobj = _context.Users.SingleOrDefault(s => s.Id == oParam.UserId);
                strListUsersInRole += "[" + userobj.UserName + "] ";
            }
            return strListUsersInRole;
        }

        public IList<Roles> Roles { get;set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> RoleQuery = from m in _roleManager.Roles orderby m.Name select m.Name;
            IQueryable<string> UsersQuery = from u in _context.Users orderby u.UserName select u.UserName;

            RolesSelectList = new SelectList(await RoleQuery.Distinct().ToListAsync());
            UsersSelectList = new SelectList(await UsersQuery.Distinct().ToListAsync());
            // Get all the roles 
            var roles = from r in _roleManager.Roles
                        select r;
            Listroles = await roles.ToListAsync();

        }
        public async Task<IActionResult> OnPostAsync(string selectedusername, string selectedrolename)
        {
            //When the Assign button is pressed 
            if ((selectedusername == null) || (selectedrolename == null))
            {
                return RedirectToPage("Manage");
            }

            ApplicationUser AppUser = _context.Users.SingleOrDefault(u => u.UserName == selectedusername);
            Roles AppRole = await _roleManager.FindByNameAsync(selectedrolename);

            IdentityResult roleResult = await _userManager.AddToRoleAsync(AppUser, AppRole.Name);

            if (roleResult.Succeeded)
            {
                TempData["message"] = "Role added to this user successfully";
                return RedirectToPage("Manage");
            }

            return RedirectToPage("Manage");
        }

        public async Task<IActionResult> OnPostDeleteUserRoleAsync(string delusername, string delrolename)
        {
            //When the Delete this user from  Role button is pressed 
            if ((delusername == null) || (delrolename == null))
            {
                return RedirectToPage("Manage");
            }

            ApplicationUser user = _context.Users.Where(u => u.UserName.Equals(delusername)).FirstOrDefault();

            if (await _userManager.IsInRoleAsync(user, delrolename))
            {
                await _userManager.RemoveFromRoleAsync(user, delrolename);

                TempData["message"] = "Role removed from this user successfully";
            }

            return RedirectToPage("Manage");
        }
    }
}
