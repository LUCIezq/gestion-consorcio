using PracticaParcial.Models.Expensas.DTO;
using PracticaParcial.Persistence.Consorcios;
using PracticaParcial.Persistence.Expensas;

namespace PracticaParcial.Models.Expensas;

public class ExpensasService : IExpensasService
{
    private readonly IExpensasRepository _expensasRepository;

    public ExpensasService(IExpensasRepository expensasRepository)
    {
        _expensasRepository = expensasRepository;
    }

    public List<ExpensasViewModel> ObtenerExpensas(int consorcioId)
    {
        return this._expensasRepository.ObtenerTodas(consorcioId);
    }

    public ResumenExpensasViewModel ObtenerResumenMesActual(int consorcioId)
    {
        return this._expensasRepository.ObtenerResumenMesActual(consorcioId);
    }
}
