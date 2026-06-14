using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Consorcios.DTOs
{
    public class ConsorcioEditarViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public required string Nombre { get; set; }
    }
}