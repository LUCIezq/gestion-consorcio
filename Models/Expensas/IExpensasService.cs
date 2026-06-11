using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Models.Expensas;

public interface IExpensasService
{
    List<ExpensasViewModel> ObtenerExpensas(int consorcioId);
    ResumenExpensasViewModel ObtenerResumenMesActual(int consorcioId);
}
