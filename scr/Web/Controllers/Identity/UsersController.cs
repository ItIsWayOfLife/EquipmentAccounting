using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Web.Models.Users;

namespace Web.Controllers.Identity
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;

        private const string CONTROLLER_NAME = "users";

        public UsersController(UserManager<ApplicationUser> userManager,
              IUserHelper userHelper)
        {
            _userManager = userManager;
            _userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult Index(string searchSelectionString, string searchString)
        {
            var listUsers = _userManager.Users;
            var listViewUsers = new List<UserViewModel>();

            foreach (var listUser in listUsers)
            {
                listViewUsers.Add(
                    new UserViewModel()
                    {
                        Id = listUser.Id,
                        Email = listUser.Email,
                        FLP = $"{listUser.SecondName} {listUser.FirstName} {listUser.MiddleName}",
                        DateOfBirth = listUser.DateOfBirth,
                        Address = listUser.Address,
                        Phone = listUser.Phone,
                        Sex = listUser.Sex
                    });
            }

            // list search
            List<string> searchSelection = new List<string>() { "Поиск", "Email", "ФИО", "Дате рождения", "Адресу", "Телефону" };

            searchString = searchString ?? string.Empty;

            // search
            if (searchSelectionString != string.Empty && searchSelectionString != null && searchSelectionString != "Поиск" && searchString != null)
            {
                if (searchSelectionString.ToLower() == searchSelection[1].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Email != null && p.Email.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[1].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Email == null || p.Email == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[2].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.FLP != null && p.FLP.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[2].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.FLP == null || p.FLP == string.Empty || p.FLP == "  ").ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[3].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.DateOfBirth.ToString("dd.MM.yyyy").ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[3].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = null;
                }
                else if (searchSelectionString.ToLower() == searchSelection[4].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Address != null && p.Address.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[4].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Address == null || p.Address == string.Empty).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[5].ToLower() && searchString != string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Phone != null && p.Phone.ToLower().Contains(searchString.ToLower())).ToList();
                }
                else if (searchSelectionString.ToLower() == searchSelection[5].ToLower() && searchString == string.Empty)
                {
                    listViewUsers = listViewUsers.Where(p => p.Phone == null || p.Phone == string.Empty).ToList();
                }
            }

            return View(new UserFilterListViewModel()
            {
                ListUsers = new ListUserViewModel { Users = listViewUsers },
                SearchSelection = new SelectList(searchSelection),
                SearchString = searchString,
                SearchSelectionString = searchSelectionString
            });
        }

        [HttpGet]
        public IActionResult Create(string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    MiddleName = model.MiddleName,
                    Sex = model.Sex,
                    DateOfBirth = model.DateOfBirth,
                    Phone = model.Phone,
                    Address = model.Address
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id, string searchSelectionString, string searchString)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
            }

            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                MiddleName = user.MiddleName,
                Sex = user.Sex,
                DateOfBirth = user.DateOfBirth,
                Phone = user.Phone,
                Address = user.Address
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FirstName = model.FirstName;
                    user.SecondName = model.SecondName;
                    user.MiddleName = model.MiddleName;
                    user.Sex = model.Sex;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Phone = model.Phone;
                    user.Address = model.Address;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { searchSelectionString, searchString });
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

        [HttpPost]
        public async Task<IActionResult> Delete(string id, string searchSelectionString, string searchString)
        {
            if (id == GetCurrentUserId())
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Вы не можете удалить этого пользователя" });
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", new { searchSelectionString, searchString });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = $"{result.Errors}" });
                }
            }

            return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = $"Пользователь не найден" });
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Error", "Home", new { requestId = "400", errorInfo = "Пользователь не найден" });
            }

            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, string searchSelectionString, string searchString)
        {
            ViewBag.SearchSelectionString = searchSelectionString;
            ViewBag.SearchString = searchString;

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    var resultRemove = await _userManager.RemovePasswordAsync(user);
                    var result = await _userManager.AddPasswordAsync(user, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", new { searchSelectionString, searchString });
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
