using FurnishedHome.Entities;
using FurnishedHome.Services;
using FurnishedHome.Utilities;
using FurnishedHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FurnishedHome.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.GetUserByEmailAsync(model.Email);
                if (user == null) ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                var password = Helper.EncodePassword(model.Password, user.SecureCode);
                if (password != user.Password)
                {
                    //ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    return RedirectToAction("Error", "Shared");
                }
                await Authenticate(user); // аутентификация
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.GetUserByEmailAsync(model.Email);

                var keyNew = Helper.GeneratePassword(10);
                var password = Helper.EncodePassword(model.Password, keyNew);
                if (user == null)
                {
                    // добавляем админа в бд

                    var admin = _roleService.GetRole("Admin");
                    if (admin == null)
                    {
                        Role adminRole = new Role()
                        {
                            Name = "Admin"
                        };
                        _roleService.AddRole(adminRole);
                        user = new User() { Email = model.Email, Password = password, RoleId = _roleService.GetRole("Admin").Id, SecureCode = keyNew };
                        // добавляем пользователя в бд
                        _userService.AddUser(user);
                    }
                    else
                    {
                        Role userRole = await _roleService.GetRoleAsync("User");
                        if (userRole == null)
                        {
                            userRole = new Role()
                            {
                                Name = "User"
                            };
                            _roleService.AddRole(userRole);
                        }
                        // добавляем пользователя в бд
                        user = new User() { Email = model.Email, Password = model.Password, RoleId = _roleService.GetRole("User").Id, SecureCode = keyNew };
                        _userService.AddUser(user);
                    }

                    await Authenticate(user); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _roleService.GetRoleById(user.RoleId).Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
