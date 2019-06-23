using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetIdentiy.Controllers
{
    public class ClaimController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public ClaimController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddClaimToUser(string username)
        {
            var user = userManager.FindByNameAsync(username).Result;
            //فرض کنید این کاربر 300 روز پیش ثبت نام کرده و این کلیم در زمان ثبت نام به او داده شده
            var claim = new Claim("RegistrationDateClaim", System.DateTime.Now.AddDays(-300).ToString());
            var result = userManager.AddClaimAsync(user, claim).Result;

            return View();
        }
    }
}