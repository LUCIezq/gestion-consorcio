
using PracticaParcial.Models.Auth.Login;
using PracticaParcial.Models.Auth.Login.DTOs;
using PracticaParcial.Models.Auth.Register;
using PracticaParcial.Models.Auth.Register.DTOs;

namespace PracticaParcial.Models.Auth
{
    public interface IAuthService
    {
        //Este metodo deberia funcionar con la base de datos, pero por ahora lo dejaremos asi
        // Task<bool> RegisterAsync(string email, string password);

        Task<RegisterResponse> Register(RegisterViewModel model);

        Task<LoginResponse> Login(LoginViewModel model);
    }
}