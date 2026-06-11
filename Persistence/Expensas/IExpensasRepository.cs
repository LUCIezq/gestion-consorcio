using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Persistence.Expensas;

public interface IExpensasRepository
{
    ResumenExpensasViewModel ObtenerResumenMesActual(int consorcioId);
    List<ExpensasViewModel> ObtenerTodas(int idConsorcio);
}
