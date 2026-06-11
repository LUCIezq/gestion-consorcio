using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.Persistence.Expensas;

public class ExpensasRepository : IExpensasRepository
{
    private readonly UnidadDbContext context;

    public ExpensasRepository(UnidadDbContext context)
    {
        this.context = context;
    }

    public async Task<ResumenExpensasViewModel> ObtenerResumenMesActual(int consorcioId)
    {
        var hoy = DateTime.Now;
        var unidades = await context.Unidades.CountAsync(u => u.Consorcio.Id == consorcioId);

        var totalMes = await context.Gastos.Where(g =>
        g.IdConsorcio == consorcioId &&
        g.FechaGasto.Year == hoy.Year &&
        g.FechaGasto.Month == hoy.Month).SumAsync(g => g.Monto);

        return new ResumenExpensasViewModel
        {
            TotalMesActual = totalMes,
            CantidadUnidades = unidades,
            MontoPorUnidades = unidades == 0 ? 0 : totalMes / unidades
        };
    }

    public async Task<List<ExpensasViewModel>> ObtenerTodas(int idConsorcio)
    {
        var gastos = context.Gastos.Where(g => g.IdConsorcio == idConsorcio);
        var cantidadDeUnidades = await context.Unidades.CountAsync(u => u.Consorcio.Id == idConsorcio);
        
        return await gastos.GroupBy(g => new
        {
            g.FechaGasto.Year,
            g.FechaGasto.Month
        }).Select(g => new ExpensasViewModel
        {
            Anio = g.Key.Year,
            Mes = g.Key.Month,
            TotalGastos = g.Sum(x => x.Monto),
            CantidadDeUnidades = cantidadDeUnidades,
            MontoPorUnidad = cantidadDeUnidades == 0 ? 0 : g.Sum( x => x.Monto) / cantidadDeUnidades
        })
        .OrderByDescending(x => x.Anio)
        .ThenByDescending(x => x.Mes)
        .ToListAsync();
    }
}
