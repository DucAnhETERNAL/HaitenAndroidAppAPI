using AutoMapper;
using BussinessLayer;
using HaitenWebAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Threading.Tasks;

namespace HaitenWebAPI.Controllers.Authen
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // API Đăng ký người dùng mới
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAPI([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return BadRequest("Invalid request.");
            }

            // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match.");
            }

            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _userRepository.GetUserByEmail(registerDTO.Email);
            if (existingUser != null)
            {
                return Conflict("Email is already registered.");
            }

            // Tạo người dùng mới từ DTO
            var newUser = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                Password = registerDTO.Password,  // Lưu mật khẩu mà không hash
                Role = "User",  // Bạn có thể chỉnh sửa vai trò nếu cần
                Status = "Active"
            };

            // Lưu người dùng vào cơ sở dữ liệu
            await _userRepository.Add(newUser);

            // Map từ User sang UserDTO để trả về thông tin người dùng
            var userDto = _mapper.Map<UserDTO>(newUser);

            // Trả về người dùng đã đăng ký thành công
            return Ok(new { User = userDto });
        }
    }
}
