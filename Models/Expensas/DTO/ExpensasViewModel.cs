namespace PracticaParcial.Models.Expensas.DTO;

public class ExpensasViewModel
{
    public int Anio { get; set; }
    public int Mes {  get; set; }
    public decimal TotalGastos { get; set; }
    public int CantidadDeUnidades { get; set; }
    public decimal MontoPorUnidad { get; set; }
}
