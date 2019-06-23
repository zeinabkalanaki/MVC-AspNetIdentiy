using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetIdentiy.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;


        public UsersController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
 
        }
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();

            return View(users);
        }

        //https://localhost:44301/users/CreateUser/?name=Reyhane&email=zeinabkalanaki@yahoo.com&password=123456
        public IActionResult CreateUser(string name, string email, string password)
        {
           var result = 
                userManager
                    .CreateAsync(
                        new AppUser
                        {
                            UserName = name,
                            Email = email
                        },
                        password)
                    .Result; // --> important
           
            return RedirectToAction("Index");
        }

        //https://localhost:44301/users/UpdateUser/?name=Reyhane
        public IActionResult UpdateUser(string name)
        {
            var user =
                userManager
                    .FindByNameAsync(name)
                    .Result;

            user.UserName = name + "_updated";

            var updatedResult =
                userManager
                    .UpdateAsync(user)
                    .Result;

            return RedirectToAction("Index");
        }

        //https://localhost:44301/users/DeleteUser/?name=Reyhane
        public IActionResult DeleteUser(string name)
        {
            var user = 
                userManager
                    .FindByNameAsync(name)
                    .Result;

            var deleteResult = 
                userManager
                    .DeleteAsync(user)
                    .Result;

            return RedirectToAction("Index");
        }


    }
}