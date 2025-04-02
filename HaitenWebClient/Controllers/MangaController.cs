using HaitenWebAPI.DTOs.Manga;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace HaitenWebClient.Controllers
{
    public class MangaController : Controller
    {

        private readonly HttpClient _httpClient;

        public MangaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult ReaddingMangaText()
        {
            return View();
        }

        public IActionResult ListMangaImage()
        {
            return View();
        }

        public async Task<IActionResult> MangaDetails(int? id)
        {
            int mangaId = id ?? 1;
            int userId = 1; // Có thể lấy từ session hoặc token

            // Gọi API để lấy thông tin Manga
            var response = await _httpClient.GetAsync($"https://localhost:7016/api/Manga/{mangaId}?userId={userId}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Không tìm thấy manga");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var mangaDto = JsonConvert.DeserializeObject<MangaDTO>(jsonString);

            ViewData["MangaId"] = mangaId;


            return View(mangaDto);
        }

    }
}
