using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Persistence.Expensas;

public interface IExpensasRepository
{
    Task<ResumenExpensasViewModel> ObtenerResumenMesActual(int consorcioId);
    Task<List<ExpensasViewModel>> ObtenerTodas(int idConsorcio);
}
