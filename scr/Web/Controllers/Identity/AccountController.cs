using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.Account;

namespace Web.Controllers.Identity
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserHelper _userHelper;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserHelper userHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userHelper = userHelper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    user = _userManager.Users.FirstOrDefault(p => p.Email == model.Email);

                    if (user == null)
                    {
                        return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                    }

                    // check if the URL belongs to the application
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userManager.Users.FirstOrDefault(p => p.Id == GetCurrentUserId());

                if (user == null)
                {
                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                // delete authentication cookies
                await _signInManager.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не аутентифицирован" });
            }
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                ProfileViewModel userView = new ProfileViewModel()
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    MiddleName = user.MiddleName,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    Sex = user.Sex,
                    Email = user.Email
                };

                return View(userView);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Edit()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                ProfileViewModel userView = new ProfileViewModel()
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    MiddleName = user.MiddleName,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    Sex = user.Sex,
                    Email = user.Email
                };

                return View(userView);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(GetCurrentUserId());

                    if (user != null)
                    {
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        user.FirstName = model.FirstName;
                        user.SecondName = model.SecondName;
                        user.MiddleName = model.MiddleName;
                        user.Sex = model.Sex;
                        user.DateOfBirth = model.DateOfBirth;
                        user.Address = model.Address;
                        user.Phone = model.Phone;

                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Profile");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                    }
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = _userHelper.GetUserById(GetCurrentUserId());

                if (user == null)
                {
                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
                }

                ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, "Неверный пароль");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Такой пользователь не найден");
                }
            }

            return View(model);
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
