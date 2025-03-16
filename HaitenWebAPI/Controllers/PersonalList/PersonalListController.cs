using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BussinessLayer;  // Assuming you have business logic in this layer
using Repository;  // Assuming you have repository layer for database interaction
using HaitenWebAPI.DTOs.UserMangaList;  // Assuming DTOs are in this namespace

namespace HaitenWebAPI.Controllers.PersonalList
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalListController : ControllerBase
    {
        private readonly IUserMangaListRepository _userMangaListRepository;  // Dependency injection for the repository
        private readonly IMangaRepository _mangaRepository;
        private readonly IUserRepository _userRepository;
        public PersonalListController(IUserMangaListRepository userMangaListRepository, IMangaRepository mangaRepository, IUserRepository userRepository)
        {
            _userMangaListRepository = userMangaListRepository;  // Initialize the repository
            _mangaRepository= mangaRepository;
            _userRepository = userRepository;
        }

        // POST api/personalList
       
        [HttpPost("add")]
        public async Task<IActionResult> AddToUserMangaList([FromBody] AddUserMangaListRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data.");
            }

            // Validate if Manga exists
            var manga = await _mangaRepository.GetById(request.MangaId);
            if (manga == null)
            {
                return NotFound("Manga not found.");
            }

            // Validate if User exists
            var user = await _userRepository.GetById(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create the UserMangaList entity
            var userMangaList = new UserMangaList
            {
                UserId = request.UserId,
                MangaId = request.MangaId,
                IsFavorite = request.IsFavorite
            };

            // Call repository to add the manga to the user's list
            await _userMangaListRepository.Add(userMangaList);

           
                return Ok(new { message = "Manga added to your list successfully." });
          
        }


        // GET api/personalList/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserMangaListDTO>>> GetUserMangaList(int userId)
        {
            // Retrieve the user's manga list
            var mangaList = await _userMangaListRepository.GetUserMangaList(userId);

            if (mangaList == null || !mangaList.Any())
            {
                return NotFound("No manga found for this user.");
            }

            // Map UserMangaList to UserMangaListDTO
            var mangaListDTO = mangaList.Select(uml => new UserMangaListDTO
            {
                Id = uml.Id,                    
                UserId = uml.UserId,            
                MangaId = uml.MangaId,        
                AddedAt = uml.AddedAt,          
                IsFavorite = uml.IsFavorite   
            }).ToList();

            // Return the mapped DTO list
            return Ok(mangaListDTO);
        }
        [HttpDelete("remove/{userMangaListId}")]
        public async Task<IActionResult> RemoveFromUserMangaList(int userMangaListId)
        {
            // Validate that the ID is greater than 0
            if (userMangaListId <= 0)
            {
                return BadRequest("Invalid data.");
            }

            // Check if the UserMangaList entry exists by its ID
            var userMangaList = await _userMangaListRepository.GetById(userMangaListId);
            if (userMangaList == null)
            {
                return NotFound("Manga not found in user's list.");
            }

            // Remove the manga from the user's list
            await _userMangaListRepository.Delete(userMangaListId);

            return Ok(new { message = "Manga removed from your list successfully." });
        }




    }
}
