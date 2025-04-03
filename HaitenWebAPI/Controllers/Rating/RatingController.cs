using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.Rate;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace HaitenWebAPI.Controllers.Rating
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRateRepository _ratingRepository;  // Interface to interact with the rating data
        private readonly IMangaRepository _mangaRepository;  // Interface to get Manga data
        private readonly IUserRepository _userRepository;    // Interface to get User data
        private readonly IMapper _mapper;  // AutoMapper for mapping

        public RatingController(
            IRateRepository ratingRepository,
            IMangaRepository mangaRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mangaRepository = mangaRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // POST api/rating/add
        [HttpPost("add")]
        public async Task<IActionResult> AddRating([FromBody] AddRateRequest addRateRequest)
        {
            // Validate if Manga exists
            var manga = await _mangaRepository.GetById(addRateRequest.MangaId);
            if (manga == null)
            {
                return NotFound("Manga not found.");
            }

            // Validate if User exists
            var user = await _userRepository.GetById(addRateRequest.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create a new Rating entity
            var rating = new Rate
            {
                UserId = addRateRequest.UserId,
                MangaId = addRateRequest.MangaId,
                Rating = addRateRequest.Rating,
                Comment = addRateRequest.Comment,
                CreatedAt = addRateRequest.CreatedAt
            };

            // Save the Rating to the database
            await _ratingRepository.Add(rating);

            // Optionally, you could also update the average rating for the manga here

            return Ok(new { message = "Rating and comment added successfully." });
        }

        [HttpGet("{mangaId}")]
        public async Task<IActionResult> GetRatingsByMangaId(int mangaId)
        {
            // Kiểm tra manga có tồn tại không
            var manga = await _mangaRepository.GetById(mangaId);
            if (manga == null)
            {
                return NotFound("Manga not found.");
            }

            // Lấy danh sách đánh giá từ repository
            var ratings = await _ratingRepository.GetByMangaId(mangaId);

            // Chuyển đổi sang DTO để trả về
            var ratingDTOs = _mapper.Map<IEnumerable<RateDTO>>(ratings);

            return Ok(ratingDTOs);
        }
    }
}
