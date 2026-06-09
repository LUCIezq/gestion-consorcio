using PracticaParcial.Models.Unidades;
using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Gastos.DTO;

public class GastoViewModel
{
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    public string? Descripcion { get; set; }
    [Required]
    public decimal Importe { get; set; }
    [Required]
    public DateOnly Fecha { get; set; }
    public int ConsorcioId { get; set; }

    public Gasto ToEntity()
    {
        return new Gasto
        {
            Id= this.Id,
            Nombre = this.Nombre,
            Descripcion = this.Descripcion,
            Importe = this.Importe,
            Fecha = this.Fecha,
            ConsorcioId = 1
        };
    }
}
