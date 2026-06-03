
using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Users;
using PracticaParcial.Persistence;
using PracticaParcial.Persistence.Auth;

namespace PracticaParcial.Persistence.Auth
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UnidadDbContext _context;

        public AuthRepository(UnidadDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task GuardarUsuario(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}