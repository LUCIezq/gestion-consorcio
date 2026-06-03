

using PracticaParcial.Models.Users;

namespace PracticaParcial.Persistence.Auth
{
    public interface IAuthRepository
    {
        Task GuardarUsuario(User user);
        Task<User?> GetUserByEmail(string email);
    }
}