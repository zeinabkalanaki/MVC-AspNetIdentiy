using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentiy.Controllers
{
    [Authorize] // just check authentication
    public class SafeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}