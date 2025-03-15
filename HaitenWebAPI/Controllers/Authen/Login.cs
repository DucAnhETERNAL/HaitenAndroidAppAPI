using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Threading.Tasks;

using HaitenWebAPI.DTOs.User;

namespace eBookStoreWebAPI.Controllers.Authen
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // API Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> LoginAPI([FromBody] LoginDTO loginDTO)
        {
            // Kiểm tra dữ liệu nhập vào
            if (loginDTO == null ||
                string.IsNullOrEmpty(loginDTO.UserNameOrEmail) ||
                string.IsNullOrEmpty(loginDTO.Password))
            {
                return BadRequest(new LoginResponse
                {
                    UserDTO = null,
                    IsAdmin = false
                });
            }

            // Gọi repository để kiểm tra đăng nhập
            var loginUser = await _userRepository.Login(loginDTO.UserNameOrEmail, loginDTO.Password);

            // Kiểm tra nếu người dùng tồn tại
            if (loginUser != null)
            {
                // Chuyển đổi từ User sang UserDTO
                var userDto = _mapper.Map<UserDTO>(loginUser);

                // Kiểm tra quyền admin nếu cần, ở đây giả sử bạn kiểm tra dựa trên Role của người dùng
                bool isAdmin = userDto.Role == "Admin";

                // Trả về phản hồi với thông tin người dùng và quyền admin
                return Ok(new LoginResponse
                {
                    UserDTO = userDto,
                    IsAdmin = isAdmin
                });
            }
            else
            {
                // Trả về lỗi nếu không tìm thấy người dùng
                return Unauthorized(new LoginResponse
                {
                    UserDTO = null,
                    IsAdmin = false
                });
            }
        }
    }
}
