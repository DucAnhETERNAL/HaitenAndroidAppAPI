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
                Status = addReadingHistoryRequest.Status,
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
            // Fetch the reading history by userId
            var readingHistory = await _readingHistoryRepository.GetByUserId(userId);

            if (readingHistory == null || !readingHistory.Any())
            {
                return NotFound("No reading history found for this user.");
            }

            // Map the ReadingHistory entities to ReadingHistoryDTOs
            var readingHistoryDto = readingHistory.Select(rh => new ReadingHistoryDTO
            {
                MangaId = rh.MangaId,
                ChapterId = rh.ChapterId,
                Status = rh.Status,
                ReadDate = rh.ReadDate
            }).ToList();

            return Ok(readingHistoryDto);
        }
    }
}
