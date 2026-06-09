using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Unidades.DTOs
{
    public class UnidadViewModel
    {
        public int IdUnidad { get; set; }

        [Required(ErrorMessage = "El identificador de la unidad es obligatorio.")]
        [StringLength(50, ErrorMessage = "El identificador no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre del propietario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string NombrePropietario { get; set; }

        [Required(ErrorMessage = "El apellido del propietario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string ApellidoPropietario { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un formato de correo válido.")]
        public string EmailPropietario { get; set; }
        [Required(ErrorMessage = "La superficie es obligatoria.")]
        public int? Superficie { get; set; }

        public DateOnly? FechaCreacion { get; set; }

        public int IdConsorcio { get; set; }
        public string? NombreConsorcio { get; set; }

        public Unidad ToEntity()
        {
            return new Unidad
            {
                IdUnidad = this.IdUnidad,
                Nombre = this.Nombre,
                NombrePropietario = this.NombrePropietario,
                ApellidoPropietario = this.ApellidoPropietario,
                EmailPropietario = this.EmailPropietario,
                Superficie = this.Superficie.Value,
            };
        }

        public static UnidadViewModel FromEntity(Unidad unidad)
        {
            return new UnidadViewModel
            {
                IdUnidad = unidad.IdUnidad,
                NombreConsorcio = unidad.Consorcio != null ? unidad.Consorcio.Nombre : "Sin Consorcio",
                Nombre = unidad.Nombre,
                NombrePropietario = unidad.NombrePropietario,
                ApellidoPropietario = unidad.ApellidoPropietario,
                EmailPropietario = unidad.EmailPropietario,
                Superficie = unidad.Superficie,
            };
        }
    }
}
