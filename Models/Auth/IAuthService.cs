using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticaParcial.Models.Auth.Login;
using PracticaParcial.Models.Auth.Register;

namespace PracticaParcial.Models.Auth
{
    public interface IAuthService
    {
        //Este metodo deberia funcionar con la base de datos, pero por ahora lo dejaremos asi
        // Task<bool> RegisterAsync(string email, string password);

        Task<RegisterResponse> Register(RegisterViewModel model);

        LoginResponse Login(LoginViewModel model);
    }
}