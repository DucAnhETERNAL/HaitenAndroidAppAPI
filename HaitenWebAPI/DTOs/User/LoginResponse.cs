namespace HaitenWebAPI.DTOs.User
{
    public class LoginResponse

    {
        public UserDTO UserDTO { get; set; }
        public bool IsAdmin { get; set; }
    }
}
