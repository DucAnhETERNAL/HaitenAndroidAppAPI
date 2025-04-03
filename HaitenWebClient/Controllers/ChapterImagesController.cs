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

        public IActionResult Index(int mangaId)
        {
            // Logic để tải trang ChapterImages dựa trên MangaId
            // Giả sử bạn muốn làm gì đó với MangaId ở đây

            // Truyền MangaId vào ViewData
            ViewData["MangaId"] = mangaId;

            // Return view của bạn (có thể là một trang khác chứa hình ảnh chapters)
            return View();
        }
    }
}
