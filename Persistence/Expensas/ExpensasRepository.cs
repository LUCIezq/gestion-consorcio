using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Persistence.Expensas;

public class ExpensasRepository // : IExpensasRepository
{
    private readonly UnidadDbContext context;

    public ExpensasRepository(UnidadDbContext context)
    {
        this.context = context;
    }
    /*
    public List<ExpensasViewModel> ObtenerTodas(int idConsorcio)
    {
        var CantidadDeUnidades = context.Unidades.Count(u => u.);
    }*/
}
