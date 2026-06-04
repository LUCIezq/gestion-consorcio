using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Auth.Login.DTOs
{
    public record LoginViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}