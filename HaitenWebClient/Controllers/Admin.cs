using Microsoft.AspNetCore.Mvc;

namespace HaitenWebClient.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
