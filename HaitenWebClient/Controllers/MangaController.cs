using HaitenWebAPI.DTOs.Chapter;
using HaitenWebAPI.DTOs.ChapterText;
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
        public async Task<IActionResult> ReaddingMangaText(int chapterId)
        {
            int userId = 1; // Có thể lấy từ session hoặc token

            // Gọi API để lấy nội dung chapter
            var chapterResponse = await _httpClient.GetAsync($"https://localhost:7016/api/ChapterText/{chapterId}");
            if (!chapterResponse.IsSuccessStatusCode)
            {
                return NotFound("Không tìm thấy nội dung chapter");
            }

            var chapterJsonString = await chapterResponse.Content.ReadAsStringAsync();
            var chapterTextDto = JsonConvert.DeserializeObject<ChapterTextDTO>(chapterJsonString);

            // Gọi API để lấy thông tin chapter (bao gồm MangaId)
            var chapterInfoResponse = await _httpClient.GetAsync($"https://localhost:7016/api/Chapter/{chapterId}");
            if (!chapterInfoResponse.IsSuccessStatusCode)
            {
                return NotFound("Không tìm thấy thông tin chapter");
            }

            var chapterInfoJsonString = await chapterInfoResponse.Content.ReadAsStringAsync();
            var chapterDto = JsonConvert.DeserializeObject<ChapterDTO>(chapterInfoJsonString);

            // Lấy MangaId từ chapterDto
            int mangaId = chapterDto.MangaId;

            // Gọi API để lấy danh sách các chương của manga
            var mangaChaptersResponse = await _httpClient.GetAsync($"https://localhost:7016/api/Manga/{mangaId}?userId={userId}");
            if (!mangaChaptersResponse.IsSuccessStatusCode)
            {
                return NotFound("Không tìm thấy danh sách chương");
            }

            var mangaChaptersJsonString = await mangaChaptersResponse.Content.ReadAsStringAsync();

            // Nếu API trả về một đối tượng thay vì một mảng, dùng ChapterDTO thay vì List<ChapterDTO>
            var mangaChaptersDto = JsonConvert.DeserializeObject<ChapterDTO>(mangaChaptersJsonString);

            // Truyền dữ liệu chapterTextDto và mangaChaptersDto vào View
            ViewData["MangaChapters"] = mangaChaptersDto.Chapters;
            ViewData["CurrentChapterId"] = chapterId;

            // Tìm chương trước và chương sau (nếu có)
            var currentChapterIndex = mangaChaptersDto.Chapters.FindIndex(ch => ch.Id == chapterId);

            ChapterDTO previousChapter = null;
            ChapterDTO nextChapter = null;
            ChapterDTO currentChapter = null;

            // Lấy chương hiện tại
            if (currentChapterIndex >= 0)
            {
                currentChapter = mangaChaptersDto.Chapters[currentChapterIndex];
            }

            // Tìm chương trước và chương sau
            if (currentChapterIndex > 0)
            {
                previousChapter = mangaChaptersDto.Chapters[currentChapterIndex - 1];
            }

            if (currentChapterIndex < mangaChaptersDto.Chapters.Count - 1)
            {
                nextChapter = mangaChaptersDto.Chapters[currentChapterIndex + 1];
            }

            // Truyền dữ liệu các chương vào ViewData
            ViewData["CurrentChapter"] = currentChapter;
            ViewData["PreviousChapter"] = previousChapter;
            ViewData["NextChapter"] = nextChapter;

            return View(chapterTextDto);
        }






        public IActionResult ListMangaImage()
        {
            return View();
        }

        public async Task<IActionResult> MangaDetails(int id)
        {
            int mangaId = id ;

            // Gọi API để lấy thông tin Manga
            var response = await _httpClient.GetAsync($"https://localhost:7016/api/Manga/{mangaId}");
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
