using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Persistence.Expensas;

public interface IExpensasRepository
{
    List<ExpensasViewModel> ObtenerTodas(int idConsorcio);
}
