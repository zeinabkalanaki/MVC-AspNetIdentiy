using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentiy.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(roleManager.Roles.ToList());
        }

        //https://localhost:44301/Role/CreateRole?Name=Staff
        public IActionResult CreateRole(string name)
        {
            var result = roleManager.CreateAsync(new IdentityRole
            {
                Name = name
            }).Result;

            return RedirectToAction("Index");
        }

        //https://localhost:44301/role/AddUserToRole?username=Reyhane_updated&roleName=admin
        public IActionResult AddUserToRole(string username, string roleName)
        {
            var user = userManager.FindByNameAsync(username).Result;

            var result = userManager.AddToRoleAsync(user, roleName).Result;

            return RedirectToAction("Index");
        }

        //https://localhost:44301/role/RemoveUserFromRole?username=Reyhane_updated&roleName=admin
        public IActionResult RemoveUserFromRole(string username, string roleName)
        {
            var user = userManager.FindByNameAsync(username).Result;

            var result = userManager.RemoveFromRoleAsync(user, roleName).Result;

            return RedirectToAction("Index");
        }
    }
}