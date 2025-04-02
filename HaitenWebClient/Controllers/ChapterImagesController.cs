using Microsoft.AspNetCore.Mvc;

namespace HaitenWebClient.Controllers
{
    public class ChapterImagesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChapterImagesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
