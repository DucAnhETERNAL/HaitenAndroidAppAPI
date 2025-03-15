using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository; // Assuming you have a repository for ChapterText
using BussinessLayer; // Assuming you have a ChapterText entity
using HaitenWebAPI.DTOs; // Assuming ChapterTextDTO is in this namespace
using System.Linq;
using System.Threading.Tasks;
using HaitenWebAPI.DTOs.ChapterText;
using HaitenWebAPI.DTOs.Comment;
using Repository.Repo;

namespace HaitenWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterTextController : ControllerBase
    {
        private readonly IChapterTextRepository _chapterTextRepository;
        private readonly ICommentRepository _commentRepository;  // Assume you have a ChapterTextRepository to fetch data
        private readonly IMapper _mapper;

        public ChapterTextController(IChapterTextRepository chapterTextRepository,ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _chapterTextRepository = chapterTextRepository;
            _mapper = mapper;
        }

        [HttpGet("{chapterId}")]
        public async Task<ActionResult<ChapterTextDTO>> GetChapterTextByChapterId(int chapterId)
        {
            // Fetch the chapter text by ChapterId
            var chapterText = await _chapterTextRepository.GetByChapterId(chapterId);

            if (chapterText == null)
            {
                return NotFound("Chapter text not found for this ChapterId.");
            }

            // Fetch comments associated with the ChapterId
            var comments = await _commentRepository.GetCommentsByChapterId(chapterId);

            // Map the chapter text entity to ChapterTextDTO
            var chapterTextDto = _mapper.Map<ChapterTextDTO>(chapterText);

            // Map the comments to CommentDTO and include them in the ChapterTextDTO
            chapterTextDto.Comments = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UserName = c.User.UserName  // Assuming User entity has a Name property
            }).ToList();

            // Return the ChapterTextDTO along with the comments
            return Ok(chapterTextDto);
        }

    }
}
