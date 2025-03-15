using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BussinessLayer;  // Assuming you have business logic
using Repository;  // Assuming you have repository layer for database interaction
using HaitenWebAPI.DTOs.Comment;  // Assuming AddCommentRequest is in this namespace

namespace HaitenWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;  // Interface to interact with comments
        private readonly IChapterRepository _chapterRepository;  // Interface to get chapters (to validate ChapterId)
        private readonly IUserRepository _userRepository;  // Interface to get users (to validate UserId)

        public CommentController(ICommentRepository commentRepository, IChapterRepository chapterRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _chapterRepository = chapterRepository;
            _userRepository = userRepository;
        }

        // POST api/comment/add
        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest addCommentRequest)
        {
            // Validate if Chapter exists
            var chapter = await _chapterRepository.GetById(addCommentRequest.ChapterId);
            if (chapter == null)
            {
                return NotFound("Chapter not found.");
            }

            // Validate if User exists
            var user = await _userRepository.GetById(addCommentRequest.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create a new Comment entity from AddCommentRequest
            var comment = new Comment
            {
                UserId = addCommentRequest.UserId,
                ChapterId = addCommentRequest.ChapterId,
                Content = addCommentRequest.Content,
                CreatedAt = addCommentRequest.CreatedAt
            };

            // Save the comment in the database
            await _commentRepository.Add(comment);

            return Ok(new { message = "Comment added successfully." });
        }
    }
}
