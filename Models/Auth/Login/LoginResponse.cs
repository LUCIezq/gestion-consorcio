using PracticaParcial.Models.Users;
namespace PracticaParcial.Models.Auth.Login

{
    public class LoginResponse
    {
        public required bool Success { get; set; }
        public required string Message { get; set; }
        public User? User { get; set; }
    }
}