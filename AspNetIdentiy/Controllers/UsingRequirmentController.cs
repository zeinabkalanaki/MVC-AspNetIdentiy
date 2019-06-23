using AspNetIdentiy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetIdentiy.Controllers
{

    public class UsingRequirmentController : Controller
    {
        private readonly IAuthorizationService authorizationService;

        public UsingRequirmentController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            var person = new Person
            {
                AddBy = 1,
                AddDate = new DateTime(2018, 01, 05),
                Id = 3,
                Name = "Person 1"
            };

            var result = authorizationService.AuthorizeAsync(User, person, "Check_EditRequirment").Result;
            if (result.Succeeded)
            {
                //edit
            }
            else
            {
                Challenge();
            }
            return View();
        }
    }
}