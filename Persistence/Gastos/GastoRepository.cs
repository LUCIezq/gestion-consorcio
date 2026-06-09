using PracticaParcial.Models.Gastos;

namespace PracticaParcial.Persistence.Gastos;

public class GastoRepository : IGastoRepository
{
    private readonly UnidadDbContext context;
    public GastoRepository(UnidadDbContext context)
    {
        this.context = context;
    }

    public void Actualizar(Gasto gasto)
    {
        this.context.Update(gasto);
        this.context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var gasto = ObtenerPorId(id);
        if (gasto != null)
        {
            context.Gastos.Remove(gasto);
            context.SaveChanges();
        }
    }

    public void Guardar(Gasto gasto)
    {
        context.Gastos.Add(gasto);
        context.SaveChanges();
    }

    public Gasto ObtenerPorId(int id)
    {
        return context.Gastos.FirstOrDefault(g => g.Id == id);
    }

    public List<Gasto> ObtenerTodos()
    {
        return context.Gastos.ToList();
    }
}
