using BussinessLayer;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Repo;

namespace HaitenWebAPI.Controllers.ChapterImage
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
        [HttpGet]
        [Route("getImagesByChapter")] //https://localhost:7016/api/chapterimages/getImagesByChapter?ChapterId=1 
        public IActionResult GetImagesByChapter(int chapterId)
        {
            var images = _chapterImagesRepository.GetImagesByChapterId(chapterId);

            if (images == null || images.Count == 0)
            {
                return NotFound($"❌ Không tìm thấy hình ảnh cho ChapterId = {chapterId}");
            }

            return Ok(images);
        }


        [HttpPost]
        [Route("uploadfile")] //https://localhost:7016/api/chapterimages/uploadfile?ChapterId=1
        public async Task<IActionResult> PostWithImage([FromForm] IFormFile Image, [FromQuery] int ChapterId, [FromQuery] int? Position)
        {
            // Kiểm tra ChapterId có hợp lệ không
            if (ChapterId == 0)
            {
                return BadRequest("❌ Lỗi: ChapterId không hợp lệ!");
            }

            var existingChapter = _chapterImagesRepository.FindChapterById(ChapterId);
            if (existingChapter == null)
            {
                return BadRequest($"❌ Lỗi: Chapter với Id = {ChapterId} không tồn tại!");
            }

            // Tiếp tục xử lý khi ChapterId hợp lệ
            if (Image == null || Image.Length == 0)
            {
                return BadRequest("❌ Lỗi: File không hợp lệ!");
            }

            // Lấy danh sách hình ảnh hiện có của Chapter
            var existingImages = _chapterImagesRepository.GetImagesByChapterId(ChapterId);

            int newPosition;
            if (Position.HasValue) // Nếu Position có giá trị được truyền vào
            {
                // Kiểm tra nếu vị trí đã tồn tại thì tìm vị trí lớn nhất + 1
                if (existingImages.Any(img => img.Position == Position.Value))
                {
                    newPosition = existingImages.Max(img => img.Position) + 1;
                }
                else
                {
                    newPosition = Position.Value;
                }
            }
            else
            {
                // Nếu không truyền Position, tự động lấy lớn nhất + 1
                newPosition = existingImages.Any() ? existingImages.Max(img => img.Position) + 1 : 1;
            }

            // Lưu file ảnh
            var fileName = Path.GetFileName(Image.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot_1", "images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            var newChapterImage = new ChapterImages
            {
                ChapterId = ChapterId,
                ImageUrl = "/images/" + fileName,
                Position = newPosition // Gán vị trí mới
            };

            _chapterImagesRepository.AddChapterImage(newChapterImage);

            return Ok(new { message = "✅ Upload thành công!", url = newChapterImage.ImageUrl, position = newChapterImage.Position });
        }
        [HttpPut]
        [Route("updateImage")] //https://localhost:7016/api/chapterimages/updateImage?Id=2
        public async Task<IActionResult> UpdateImage([FromQuery] int Id, [FromForm] IFormFile Image)
        {
            if (Id <= 0)
            {
                return BadRequest("❌ Lỗi: Id không hợp lệ!");
            }

            var existingImage = _chapterImagesRepository.GetChapterImageById(Id);
            if (existingImage == null)
            {
                return NotFound($"❌ Không tìm thấy hình ảnh với Id = {Id}");
            }

            if (Image == null || Image.Length == 0)
            {
                return BadRequest("❌ Lỗi: File không hợp lệ!");
            }

            // Xóa file cũ nếu có
            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot_1", existingImage.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            // Lưu file mới
            var fileName = Path.GetFileName(Image.FileName);
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot_1", "images", fileName);
            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            // Cập nhật URL ảnh mới
            existingImage.ImageUrl = "/images/" + fileName;
            _chapterImagesRepository.UpdateChapterImage(existingImage);

            return Ok(new { message = "✅ Cập nhật hình ảnh thành công!", url = existingImage.ImageUrl });
        }


        [HttpDelete]
        [Route("deleteImage")] //https://localhost:7016/api/chapterimages/deleteImage?id=2
        public IActionResult DeleteImage([FromQuery] int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("❌ Lỗi: Id không hợp lệ!");
            }

            var existingImage = _chapterImagesRepository.GetChapterImageById(Id);
            if (existingImage == null)
            {
                return NotFound($"❌ Không tìm thấy hình ảnh với Id = {Id}");
            }

            // Xóa file vật lý
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot_1", existingImage.ImageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Xóa ảnh khỏi database
            _chapterImagesRepository.DeleteChapterImage(Id);

            return Ok(new { message = "✅ Xóa hình ảnh thành công!" });
        }


    }
}
