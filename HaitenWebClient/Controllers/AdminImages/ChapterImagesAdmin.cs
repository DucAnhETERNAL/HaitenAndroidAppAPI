using HaitenWebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HaitenWebClient.Controllers.AdminImages
{
    public class ChapterImagesAdmin : Controller
    {
        private readonly HttpClient _httpClient;

        public ChapterImagesAdmin(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7016/api/chapterimages/");
        }

        // 🖼 Hiển thị danh sách ảnh theo ChapterId
        public async Task<IActionResult> Index(int chapterId)
        {
            var response = await _httpClient.GetAsync($"getImagesByChapter?chapterId={chapterId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Không tìm thấy hình ảnh!";
                return View(new List<ChapterImageViewModel>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var images = JsonSerializer.Deserialize<List<ChapterImageViewModel>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(images);
        }

        // 🌟 Upload ảnh lên MVC & API
        [HttpPost]
        public async Task<IActionResult> UploadImage(int ChapterId, IFormFile Image, int? Position)
        {
            if (Image == null || Image.Length == 0)
            {
                TempData["Error"] = "❌ Lỗi: File không hợp lệ!";
                return RedirectToAction("Index", new { chapterId = ChapterId });
            }

            // 📂 Lưu ảnh vào wwwroot/images của MVC
            var fileName = Path.GetFileName(Image.FileName);
            var mvcPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var stream = new FileStream(mvcPath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            // 📨 Gửi ảnh lên API
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(Image.OpenReadStream()), "Image", Image.FileName);
            var response = await _httpClient.PostAsync($"uploadfile?ChapterId={ChapterId}&Position={Position}", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "❌ Lỗi: Không thể tải ảnh lên API!";
            }
            else
            {
                TempData["Success"] = "✅ Ảnh đã được tải lên thành công!";
            }

            return RedirectToAction("Index", new { chapterId = ChapterId });
        }

        // ✏ Cập nhật ảnh
        [HttpPost]
        public async Task<IActionResult> UpdateImage(int Id, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                TempData["Error"] = "❌ Lỗi: File không hợp lệ!";
                return RedirectToAction("Index");
            }

            // 📂 Lưu ảnh vào wwwroot/images của MVC
            var fileName = Path.GetFileName(Image.FileName);
            var mvcPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using (var stream = new FileStream(mvcPath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            // 📨 Gửi ảnh lên API
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(Image.OpenReadStream()), "Image", Image.FileName);
            var response = await _httpClient.PutAsync($"updateImage?Id={Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "❌ Lỗi: Không thể cập nhật ảnh!";
            }
            else
            {
                TempData["Success"] = "✅ Ảnh đã được cập nhật thành công!";
            }

            return RedirectToAction("Index");
        }


        // 🗑 Xóa ảnh
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int Id)
        {
            var response = await _httpClient.DeleteAsync($"deleteImage?id={Id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "❌ Lỗi: Không thể xóa ảnh!";
            }
            else
            {
                TempData["Success"] = "✅ Ảnh đã được xóa!";
            }

            return RedirectToAction("Index");
        }
    }
}
