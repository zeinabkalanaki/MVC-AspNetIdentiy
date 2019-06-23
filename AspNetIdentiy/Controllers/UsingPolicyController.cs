using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AspNetIdentiy.Controllers
{
    [Authorize(Policy = "Check_RegistrationDateClaim")]
    public class UsingPolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
