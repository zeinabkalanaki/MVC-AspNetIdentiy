using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentiy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> usermanager,SignInManager<AppUser> signInManager)
        {
            this.usermanager = usermanager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        //https://localhost:44301/Account/Login?username=Reyhane_updated&password=123456
        public IActionResult Login(string username,string password)
        {
            var user = usermanager.FindByNameAsync(username).Result;

            if (user != null)
            {
                signInManager.SignOutAsync(); //for change access level

                var result = signInManager.PasswordSignInAsync(user,password, false, false).Result;

                var temp = User.Identity.IsAuthenticated;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Safe");
                }
               
            }
            return View();
        }

        //https://localhost:44301/Account/Logout
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}