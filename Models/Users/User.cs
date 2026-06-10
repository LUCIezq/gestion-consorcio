using PracticaParcial.Models.Consorcios;

namespace PracticaParcial.Models.Users
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UltimoLogin { get; set; }

        public List<Consorcio>? Consorcios { get; set; }
    }
}