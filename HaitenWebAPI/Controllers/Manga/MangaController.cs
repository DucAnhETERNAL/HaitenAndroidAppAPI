using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.Chapter;
using HaitenWebAPI.DTOs.Manga;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository;
using Repository.Repo;

namespace HaitenWebAPI.Controllers.MangaDT
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMangaRepository _mangaRepository;
        private readonly IReadingHistoryRepository _readingHistoryRepository;
        private readonly IMapper _mapper;
        public MangaController(IMangaRepository mangaRepository, IUserRepository userRepository, IReadingHistoryRepository readingHistoryRepository, IMapper mapper)
        {
            _readingHistoryRepository = readingHistoryRepository;
            _mangaRepository = mangaRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet("active")]
        [EnableQuery]
        public async Task<ActionResult<IQueryable<MangaListDTO>>> GetAllByStatus()
        {
            var ListManga = await _mangaRepository.GetAll();
            var activeMangas = ListManga.Where(manga => manga.Status == "Active");
            var ListMangaDto = activeMangas.Select(manga => _mapper.Map<MangaListDTO>(manga));
            return Ok(ListMangaDto);
        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IQueryable<MangaListDTO>>> GetAll()  
        {
            var ListManga = await _mangaRepository.GetAll();
            var ListMangaDto = ListManga.Select(manga => _mapper.Map<MangaListDTO>(manga));
            return Ok(ListMangaDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MangaDTO>> GetDetailAndChapterList(int? id, [FromQuery] int userId)
        {

            // Nếu id không được truyền vào, mặc định là 1
            int mangaId = id ?? 1;
            // Lấy thông tin manga theo id
            var manga = await _mangaRepository.GetById(mangaId); // Giả sử GetById lấy manga và các chapter của nó

            if (manga == null)
            {
                return NotFound("Manga không tồn tại.");
            }

            // Lấy thông tin lịch sử đọc của người dùng từ _readingHistoryRepository
            var readingHistory = await _readingHistoryRepository.GetByUserId(userId); // Sử dụng phương thức GetByUserId để lấy lịch sử đọc

            // Dùng AutoMapper để chuyển đổi manga sang DTO
            var mangaDto = _mapper.Map<MangaDTO>(manga);

            // Cập nhật thông tin chapters với trạng thái đọc hay chưa đọc
            mangaDto.Chapters = manga.Chapters.Select(chapter => new ChapterListDTO
            {
                Id = chapter.Id,
                Name = chapter.Name,
                ViewCount = chapter.ViewCount,
                // Kiểm tra xem người dùng đã đọc chapter này chưa, và thêm điều kiện kiểm tra MangaId
                Status = readingHistory.Any(rh => rh.ChapterId == chapter.Id && rh.MangaId == id) ? true : false // false = đã đọc, true = chưa đọc
            }).ToList();

            // Trả về mangaDto với danh sách chapters và trạng thái
            return Ok(mangaDto);
        }
        [HttpPost]
        public async Task<ActionResult<MangaDTO>> AddManga([FromBody] AddMangaRequestDTO mangaRequest)
        {
            // Validate input (you can add more validation if needed)
            if (mangaRequest == null)
            {
                return BadRequest("Manga data is required.");
            }

            // Check if the genre exists
            var genre = await _mangaRepository.GetById(mangaRequest.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid genre.");
            }

            // Create new Manga object from DTO
            var manga = new Manga
            {
                Title = mangaRequest.Title,
                Description = mangaRequest.Description,
                Author = mangaRequest.Author,
                Type = mangaRequest.Type,
                
                GenreId = mangaRequest.GenreId,
                Status = mangaRequest.Status,
            };

            // Add the new manga to the database
            await _mangaRepository.Add(manga);

            // Map the added manga to the MangaDTO
            var mangaDto = _mapper.Map<MangaDTO>(manga);

            // Return the newly created manga
            return CreatedAtAction(nameof(GetDetailAndChapterList), new { id = manga.Id }, mangaDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MangaDTO>> UpdateManga(int id, [FromBody] MangaEditDTO mangaEditRequest)
        {
            // Validate input (you can add more validation if needed)
            if (mangaEditRequest == null)
            {
                return BadRequest("Manga data is required.");
            }

            // Fetch the existing manga from the database
            var existingManga = await _mangaRepository.GetById(id);
            if (existingManga == null)
            {
                return NotFound("Manga not found.");
            }

            // Check if the genre exists (optional, if you're updating genre info)
            var genre = await _mangaRepository.GetById(mangaEditRequest.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid genre.");
            }

            // Update the manga's properties from the request DTO
            existingManga.Title = mangaEditRequest.Title ?? existingManga.Title; // Optional: Only update if not null
            existingManga.Description = mangaEditRequest.Description ?? existingManga.Description;
            existingManga.Author = mangaEditRequest.Author;
            existingManga.GenreId = mangaEditRequest.GenreId;
            existingManga.Status = mangaEditRequest.Status;

            // Update the manga in the database
            await _mangaRepository.Update(existingManga);

            // Map the updated manga to MangaDTO
            var mangaDto = _mapper.Map<MangaDTO>(existingManga);

            // Return the updated manga
            return Ok(mangaDto);
        }


    }
}
