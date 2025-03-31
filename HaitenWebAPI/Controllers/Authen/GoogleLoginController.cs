using AutoMapper;
using BussinessLayer;
using Google.Apis.Auth;
using HaitenWebAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HaitenWebAPI.Controllers.Authen
{
    [ApiController]
    [Route("api/google-login")]
    public class GoogleLoginController :ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public GoogleLoginController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                // Lấy idToken từ yêu cầu gửi đến
                string idToken = request.Token;

                // Cấu hình xác thực Google
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { "80241279325-7vv3gco6bt8rdchkh1oepa3hn1mp8bnr.apps.googleusercontent.com" } // Thay bằng Client ID của bạn
                };

                // Xác thực token
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);


                string userId = payload.Subject;
                string userEmail = payload.Email;
                string userName = payload.Name;

                var existingUser = await _userRepository.GetUserByEmail(userEmail);
                if (existingUser != null)
                {
                    // User exists, generate JWT token
                    var token = GenerateToken(existingUser);
                    return Ok(new LoginGoogleRespone { JwtToken = token });
                }
                else
                {
                    // User does not exist, create new user and generate JWT token
                    var newUser = new User
                    {
                        UserName = userName,
                        Email = userEmail,
                        Role = "User",  // Default role, you can adjust based on your logic
                        Status = "Active"
                    };

                    await _userRepository.Add(newUser);

                    // Generate JWT token for the newly created user
                    var token = GenerateToken(newUser);
                    return Ok(new LoginGoogleRespone { JwtToken = token });
                }
            }
            catch (InvalidJwtException ex)
            {
                return BadRequest(new { success = false, message = "Invalid token: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred: " + ex.Message });
            }
        }
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               _configuration["Jwt:Issuer"],
               _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiry time (e.g., 1 hour)
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public class GoogleLoginRequest
        {
            public string Token { get; set; }  // Để nhận ID token từ client
        }

    }
}
