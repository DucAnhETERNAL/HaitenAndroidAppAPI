namespace HaitenWebAPI.DTOs.User
{
    public class LoginDTO
    {
        public string UserNameOrEmail { get; set; }  // Cho phép đăng nhập bằng username hoặc email
        public string Password { get; set; }
    }
}
