using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Repository;  // Assuming you have a repository for ReadingHistory
using BussinessLayer;  // Assuming you have business layer logic
using HaitenWebAPI.DTOs.ReadingHistoryDT;  // Assuming you have DTOs for ReadingHistory

namespace HaitenWebAPI.Controllers.ReadingHistoryDT
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingHistoryController : ControllerBase
    {
        private readonly IReadingHistoryRepository _readingHistoryRepository;
        private readonly IMangaRepository _mangaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChapterRepository _chapterRepository;

        public ReadingHistoryController(
            IReadingHistoryRepository readingHistoryRepository,
            IMangaRepository mangaRepository,
            IUserRepository userRepository,
            IChapterRepository chapterRepository)
        {
            _readingHistoryRepository = readingHistoryRepository;
            _mangaRepository = mangaRepository;
            _userRepository = userRepository;
            _chapterRepository = chapterRepository;
        }

        // POST api/readinghistory/add
        [HttpPost("add")]
        public async Task<IActionResult> AddReadingHistory([FromBody] AddReadingHistoryRequest addReadingHistoryRequest)
        {
            // Validate if Manga exists
            var manga = await _mangaRepository.GetById(addReadingHistoryRequest.MangaId);
            if (manga == null)
            {
                return NotFound("Manga not found.");
            }

            // Validate if Chapter exists
            var chapter = await _chapterRepository.GetById(addReadingHistoryRequest.ChapterId);
            if (chapter == null)
            {
                return NotFound("Chapter not found.");
            }

            // Validate if User exists
            var user = await _userRepository.GetById(addReadingHistoryRequest.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create the ReadingHistory entity
            var readingHistory = new ReadingHistory
            {
                UserId = addReadingHistoryRequest.UserId,
                MangaId = addReadingHistoryRequest.MangaId,
                ChapterId = addReadingHistoryRequest.ChapterId,
                Status = "Đã Đọc",
                ReadDate = addReadingHistoryRequest.ReadDate
            };

            // Save the reading history to the database
            await _readingHistoryRepository.Add(readingHistory);

            return Ok(new { message = "Reading history added successfully." });
        }

        // GET api/readinghistory/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ReadingHistoryDTO>>> GetReadingHistoryByUserId(int userId)
        {
            // Lấy lịch sử đọc theo userId
            var readingHistory = await _readingHistoryRepository.GetByUserId(userId);

            if (readingHistory == null || !readingHistory.Any())
            {
                return NotFound("No reading history found for this user.");
            }

            // Lấy tất cả các MangaId từ lịch sử đọc
            var mangaIds = readingHistory.Select(rh => rh.MangaId).Distinct().ToList();

            // Lấy tất cả thông tin manga (bao gồm Title) cho tất cả các MangaId
            var mangas = await _mangaRepository.GetByIds(mangaIds); // Giả sử bạn có phương thức GetByIds trong MangaRepository

            // Tạo một dictionary để ánh xạ MangaId với Title
            var mangaNames = mangas.ToDictionary(m => m.Id, m => m.Title);

            // Nhóm lịch sử đọc theo MangaId và lấy Title từ dictionary
            var groupedByManga = readingHistory
                .GroupBy(rh => rh.MangaId)
                .Select(group => new ReadingHistoryDTO
                {
                    MangaId = group.Key,
                    MangaName = mangaNames.ContainsKey(group.Key) ? mangaNames[group.Key] : "Unknown", // Lấy MangaTitle từ dictionary
                    ReadDate = group.Max(rh => rh.ReadDate) // Lấy ngày đọc mới nhất
                }).ToList();

            return Ok(groupedByManga);
        }

    }
}
