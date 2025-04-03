using Microsoft.AspNetCore.Mvc;

namespace HaitenWebClient.Controllers
{
    public class ChapterController : Controller
    {
        private readonly string _apiBaseUrl = "https://localhost:7016/api/chapter";

        public IActionResult Index(int? mangaId)
        {
            // Truyền mangaId vào ViewData
            ViewData["MangaId"] = mangaId;

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
