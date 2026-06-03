using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Consorcio
{
    public class CreateConsorcioViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El campo dirección es obligatorio.")]
        [StringLength(150, ErrorMessage = "La longitud máxima para la dirección es de 150 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s,.\d\-\']+$", ErrorMessage = "La dirección contiene caracteres inválidos.")]
        public required string Calle { get; set; }

        [Required(ErrorMessage = "El campo Provincia es obligatorio.")]
        [StringLength(100, ErrorMessage = "La longitud máxima para la provincia es de 100 caracteres.")]
        public required string Provincia { get; set; }

        [Required(ErrorMessage = "El campo Partido/Ciudad es obligatorio.")]
        [StringLength(100, ErrorMessage = "La longitud máxima para la ciudad es de 100 caracteres.")]
        public required string Ciudad { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        [StringLength(20, ErrorMessage = "La longitud máxima para el código postal es de 20 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-]+$", ErrorMessage = "El código postal contiene caracteres inválidos.")]
        public required string CodigoPostal { get; set; }

        [Required(ErrorMessage = "El día de vencimiento es obligatorio.")]
        [Range(1, 28, ErrorMessage = "El día de vencimiento de expensas debe ser un valor entre 1 y 28.")]
        public required int DiaVencimientoExpensas { get; set; }

        [Required(ErrorMessage = "El campo Latitud es obligatorio.")]
        [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90 grados.")]
        public required double Latitud { get; set; }

        [Required(ErrorMessage = "El campo Longitud es obligatorio.")]
        [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180 grados.")]
        public required double Longitud { get; set; }
    }
}