using BussinessLayer;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace HaitenWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterImagesController : ControllerBase
    {
        private readonly IChapterImagesRepository _chapterImagesRepository;

        public ChapterImagesController(IChapterImagesRepository chapterImagesRepository)
        {
            _chapterImagesRepository = chapterImagesRepository;
        }

        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> PostWithImage(
            [FromForm] IFormFile Image,
            [FromQuery] int ChapterId,
            [FromQuery] int Position)
        {
            // Kiểm tra ChapterId có hợp lệ không
            if (ChapterId == 0)
            {
                return BadRequest(" Lỗi: ChapterId không hợp lệ!");
            }

            var existingChapter = _chapterImagesRepository.FindChapterByIdAsync(ChapterId);
            if (existingChapter == null)
            {
                return BadRequest($" Lỗi: Chapter với Id = {ChapterId} không tồn tại!");
            }

            // Tiếp tục xử lý khi ChapterId hợp lệ
            if (Image == null || Image.Length == 0)
            {
                return BadRequest(" Lỗi: File không hợp lệ!");
            }

            var fileName = Path.GetFileName(Image.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            var newChapterImage = new ChapterImages
            {
                ChapterId = ChapterId,
                ImageUrl = "/images/" + fileName,
                Position = Position
            };

            _chapterImagesRepository.AddChapterImageAsync(newChapterImage);

            return Ok(new { message = "✅ Upload thành công!", url = newChapterImage.ImageUrl });
            //https://localhost:7180/uploadfile?ChapterId=3&Position=1 đường link test Postman
        }

    }
}
