
using PracticaParcial.Models.Users;

namespace PracticaParcial.Repository
{
    public class AuthRepository
    {
        public List<User> users = [];

        public void Register(User user)
        {
            users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return users.FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}