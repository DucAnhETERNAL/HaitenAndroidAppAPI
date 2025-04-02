using Microsoft.AspNetCore.Mvc;

namespace HaitenWebClient.Controllers
{
    public class Authen : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginWithGoogle()
        {
            return View();
        }
        public IActionResult NewGGlogin()
        {
            return View();
        }
    }
}
