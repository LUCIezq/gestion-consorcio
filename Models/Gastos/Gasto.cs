using PracticaParcial.Models.Consorcios;

namespace PracticaParcial.Models.Gastos;

public class Gasto
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal Importe { get; set; }

    public DateOnly Fecha { get; set; }

    public string? ComprobantePath { get; set; }

    public int ConsorcioId { get; set; }

    public Consorcio Consorcio { get; set; }
}
