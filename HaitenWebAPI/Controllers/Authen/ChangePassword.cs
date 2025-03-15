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
    public class ChangePasswordController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ChangePasswordController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // API Thay đổi mật khẩu người dùng
        [HttpPut("{id:int}/change-password")]
        public async Task<IActionResult> ChangePasswordAPI(int id, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (changePasswordDTO == null)
            {
                return BadRequest("Invalid request");
            }

            // Kiểm tra xem mật khẩu mới và mật khẩu xác nhận có khớp không
            if (changePasswordDTO.NewPassword != changePasswordDTO.ConfirmNewPassword)
            {
                return BadRequest("New password and confirm password do not match.");
            }

            // Lấy thông tin người dùng từ cơ sở dữ liệu
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Kiểm tra mật khẩu hiện tại có đúng không
            if (user.Password != changePasswordDTO.CurrentPassword) // Nếu bạn hash mật khẩu, so sánh với mật khẩu đã hash
            {
                return BadRequest("Current password is incorrect.");
            }

            // Map dữ liệu từ DTO vào user model
            _mapper.Map(changePasswordDTO, user); // Chỉ map mật khẩu mới

            // Cập nhật mật khẩu mới
            await _userRepository.Update(user);

            return NoContent(); 
        }
    }
}
