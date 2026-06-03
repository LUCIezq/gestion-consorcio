using Microsoft.AspNetCore.Identity;
using PracticaParcial.Models;
using PracticaParcial.Models.Auth;
using PracticaParcial.Models.Auth.Login;
using PracticaParcial.Models.Auth.Register;
using PracticaParcial.Models.Users;
using PracticaParcial.Persistence.Auth;
using PracticaParcial.shared;

namespace PracticaParcial.Repository.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IAuthRepository authRepository, IPasswordHasher<User> passwordHasher)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterResponse> Register(RegisterViewModel model)
        {
            if (model == null) return new RegisterResponse { Success = false, Message = "Informacion de registro invalida." };

            if (await _authRepository.GetUserByEmail(model.Email) != null) return new RegisterResponse { Success = false, Message = "El mail ya se encuentra en uso, pruebe utilizando otro" };

            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email.ToLower(),
                CreatedAt = DateTime.Now
            };

            user.Password = _passwordHasher.HashPassword(user, model.Password);

            await _authRepository.GuardarUsuario(user);
            return new RegisterResponse { Success = true, Message = "Usuario registrado exitosamente." };
        }

        public LoginResponse Login(LoginViewModel model)
        {
            if (model == null)
            {
                return new LoginResponse { Success = false, Message = "Informacion de login invalida." };
            }

            User? user = _authRepository.GetUserByEmail(model.Email);

            if (user == null)
            {
                return new LoginResponse { Success = false, Message = "Email o contraseña incorrectos." };
            }

            var resultado = _passwordHasher.VerifyHashedPassword(user, user.Password!, model.Password);

            if (resultado == PasswordVerificationResult.Failed)
            {
                return new LoginResponse { Success = false, Message = "Email o contraseña incorrectos." };
            }

            return new LoginResponse { Success = true, Message = "Login exitoso.", User = user };
        }

    }
}