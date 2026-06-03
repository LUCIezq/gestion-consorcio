using System.ComponentModel.DataAnnotations;
using PracticaParcial.Models.Reserva;

namespace PracticaParcial.Models.Unidades
{
    public class Unidad
    {
        [Key]
        public int IdUnidad { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string NombrePropietario { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoPropietario { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailPropietario { get; set; }

        [Required]
        public int Superficie { get; set; }

        [Required]
        public DateOnly FechaCreacion { get; set; }

        public ICollection<ReservaSUM> Reservas { get; set; } = new List<ReservaSUM>();
    }
}
