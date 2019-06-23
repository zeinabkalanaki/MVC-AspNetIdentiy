﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentiy.Controllers
{
    [Authorize(Roles = "admin")] // is casesensitive
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}