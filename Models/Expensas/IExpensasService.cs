using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Models.Expensas;

public interface IExpensasService
{
    Task<List<ExpensasViewModel>> ObtenerExpensasAsync(int consorcioId);
    Task<ResumenExpensasViewModel> ObtenerResumenMesActualAsync(int consorcioId);
}
