using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomDbIdentity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomDbIdentity.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthorizationController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {

            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                //password = "duupa";
                var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string userName, string password)
        {

            var user = new AppUser()
            {
                Id = 0,
                UserName = userName,
                //Password = password


            };

            var result = await _userManager.CreateAsync(user,password);

            if (result.Succeeded)
            {
                //sign in
                var signIn = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signIn.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return RedirectToAction("Login");
        }
    }
}
