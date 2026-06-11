using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Expensas.DTO;
using PracticaParcial.Persistence.Expensas;

namespace PracticaParcial.Models.Expensas;

public class ExpensasService : IExpensasService
{
    private readonly IExpensasRepository _expensasRepository;

    public ExpensasService(IExpensasRepository expensasRepository)
    {
        _expensasRepository = expensasRepository;
    }

    public async Task<List<ExpensasViewModel>> ObtenerExpensasAsync(int consorcioId)
    {
        return await this._expensasRepository.ObtenerTodas(consorcioId);
    }

    public async Task<ResumenExpensasViewModel> ObtenerResumenMesActualAsync(int consorcioId)
    {
        return await this._expensasRepository.ObtenerResumenMesActual(consorcioId);
    }
}
