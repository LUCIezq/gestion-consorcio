namespace PracticaParcial.Models.Expensas.DTO;

public class VerExpensasViewModel
{
    public ResumenExpensasViewModel Resumen { get; set; } 
    public List<ExpensasViewModel> Expensas { get; set; }
    public string NombreConsorcio { get; set; }
}
