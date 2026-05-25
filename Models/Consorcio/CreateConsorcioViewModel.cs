using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Consorcio
{
    public class CreateConsorcioViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        //validar que sea string
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public required string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Provincia es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Provincia válida (valor numérico).")]
        public required int ProvinciaId { get; set; }

        [Required(ErrorMessage = "El campo Ciudad es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una Ciudad válida (valor numérico).")]
        public required int PartidoId { get; set; }

        [Required(ErrorMessage = "El campo calle es obligatorio.")]
        [StringLength(100, ErrorMessage = "La longitud máxima para la calle es de 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre de la calle solo puede contener letras y espacios.")]

        public required string Calle { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo altura es obligatorio.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La altura debe ser estrictamente numérica.")]
        public required string Altura { get; set; } = string.Empty;

        [Required(ErrorMessage = "El día de vencimiento es obligatorio.")]
        [Range(1, 31, ErrorMessage = "El día de vencimiento de expensas debe ser un valor entre 1 y 31.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La altura debe ser estrictamente numérica.")]
        public required int DiaVencimientoExpensas { get; set; }
    }
}