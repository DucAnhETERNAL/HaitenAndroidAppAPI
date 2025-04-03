using Microsoft.AspNetCore.Mvc;

namespace HaitenWebClient.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DetailAndAddChapter(int id)
        {
            
            return View(id); 
        }


    }
}
