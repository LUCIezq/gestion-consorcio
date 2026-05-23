using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models
{
    public record RegisterViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        public required string Email { get; set; }

        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmPassword { get; set; }
    }
}