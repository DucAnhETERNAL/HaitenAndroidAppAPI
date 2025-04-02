using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.Chapter;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Repo;

namespace HaitenWebAPI.Controllers.ChapterCT
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IChapterTextRepository _chapterTextRepository;
        private readonly IMapper _mapper;

        // Constructor to inject dependencies
        public ChapterController(IChapterRepository chapterRepository, IMapper mapper, IChapterTextRepository chapterTextRepository)
        {
            _chapterRepository = chapterRepository;
            _mapper = mapper;
            _chapterTextRepository = chapterTextRepository;
        }

        // POST: api/chapter
        [HttpPost]
        public async Task<ActionResult<ChapterDTO>> AddChapter([FromBody] AddChapterRequestDTO chapterRequest)
        {
            // Validate input
            if (chapterRequest == null)
            {
                return BadRequest("Chapter data is required.");
            }

            // Create a new Chapter entity
            var chapter = new Chapter
            {
                MangaId = chapterRequest.MangaId,
                Name = chapterRequest.Name,
                Status = chapterRequest.Status
            };

            // Add the chapter to the database first to generate the Id
            await _chapterRepository.Add(chapter);

            // Now that the chapter has an Id, create ChapterText from the Content
            var chapterText = new ChapterText
            {
                Content = chapterRequest.Content,
                ChapterId = chapter.Id // Associate ChapterText with the newly created Chapter
            };

            // Add the ChapterText to the database
            await _chapterTextRepository.Add(chapterText);

            // Map the created chapter to ChapterDTO
            var chapterDto = _mapper.Map<ChapterDTO>(chapter);

            // Return the newly created chapter
            return CreatedAtAction(nameof(GetChapterById), new { id = chapter.Id }, chapterDto);
        }



        // A sample method to get a chapter by ID (you would implement this method as well)
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDTO>> GetChapterById(int id)
        {
            var chapter = await _chapterRepository.GetById(id);
            if (chapter == null)
            {
                return NotFound("Chapter not found.");
            }

            var chapterDto = _mapper.Map<ChapterDTO>(chapter);
            return Ok(chapterDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ChapterDTO>> UpdateChapter(int id, [FromBody] ChapterEditDTO chapterEditRequest)
        {
            var chapter = await _chapterRepository.GetById(id);
            if (chapter == null)
            {
                return NotFound("Chapter not found.");
            }

            // Cập nhật thông tin chương từ DTO
            chapter.Name = chapterEditRequest.Name ?? chapter.Name;
            chapter.Status = chapterEditRequest.Status;

            // Cập nhật nội dung nếu có (nếu DTO có Content)
            if (!string.IsNullOrEmpty(chapterEditRequest.Content))
            {
                // Cập nhật nội dung của ChapterText
                chapter.ChapterText.Content = chapterEditRequest.Content;

                // Cập nhật ChapterText trong cơ sở dữ liệu
                await _chapterTextRepository.Update(chapter.ChapterText);
            }

            // Lưu thay đổi vào cơ sở dữ liệu cho Chapter
            await _chapterRepository.Update(chapter);

            // Map lại chapter sang ChapterDTO và trả về
            var chapterDto = _mapper.Map<ChapterDTO>(chapter);
            return Ok(chapterDto);
        }
        [HttpPut("{id}/view")]
        public async Task<ActionResult<ChapterDTO>> IncrementViewCount(int id)
        {
            // Lấy thông tin chương từ repository
            var chapter = await _chapterRepository.GetById(id);
            if (chapter == null)
            {
                return NotFound("Chapter not found.");
            }

            // Tăng ViewCount lên 1
            chapter.ViewCount += 1;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _chapterRepository.Update(chapter);

            // Map lại chapter sang ChapterDTO và trả về
            var chapterDto = _mapper.Map<ChapterDTO>(chapter);
            return Ok(chapterDto);
        }



    }
}
