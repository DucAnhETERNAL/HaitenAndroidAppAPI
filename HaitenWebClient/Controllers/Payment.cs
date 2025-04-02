using Microsoft.AspNetCore.Mvc;

namespace HaitenWebClient.Controllers
{
    public class Payment : Controller
    {
        public IActionResult Payments()
        {
            return View();
        }

    }
}
