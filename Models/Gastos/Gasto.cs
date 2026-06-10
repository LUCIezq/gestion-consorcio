using PracticaParcial.Models.Consorcios;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaParcial.Models.Gastos
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdConsorcio { get; set; }

        [Required]
        public int IdTipoGasto { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        [Required]
        public DateOnly FechaGasto { get; set; }

        [Required]
        public int AnioExpensa { get; set; }

        [Required]
        [Range(1, 12)]
        public int MesExpensa { get; set; }

        [MaxLength(250)]
        public string? ArchivoComprobante { get; set; }
    
        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Monto { get; set; }

        [ForeignKey("IdTipoGasto")]
        public virtual TipoGasto? TipoGasto { get; set; }
    }
}
