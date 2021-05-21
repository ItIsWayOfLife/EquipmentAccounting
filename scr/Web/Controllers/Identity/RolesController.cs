using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.Roles;

namespace Web.Controllers.Identity
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId, string searchSelectionString, string searchString)
        {
            // get users
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // get lost roles users
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };

                ViewBag.SearchSelectionString = searchSelectionString;
                ViewBag.SearchString = searchString;

                return View(model);
            }

            return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "User not found" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles, string searchSelectionString, string searchString)
        {
            // get users
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (user != null)
            {
                // get list roles users
                var userRoles = await _userManager.GetRolesAsync(user);

                // get list roles users, which were added
                var addedRoles = roles.Except(userRoles);

                // get roles, which have been removed
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);


                return RedirectToAction("Index", "Users", new { searchSelectionString, searchString });
            }

            return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "User not found" });
        }

        private string GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                return null;
            }
        }
    }
}
