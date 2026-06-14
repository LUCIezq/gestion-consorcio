using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Gastos;

namespace PracticaParcial.Persistence.Gastos;

public class GastoRepository : IGastoRepository
{
    private readonly UnidadDbContext _db;

    public GastoRepository(UnidadDbContext db)
    {
        _db = db;
    }

    public void Actualizar()
    {
        _db.SaveChanges();
    }

    public void Agregar(Gasto gasto)
    {
        _db.Gastos.Add(gasto);
        _db.SaveChanges();
    }

    public void Eliminar(Gasto gasto)
    {
        _db.Gastos.Remove(gasto);
        _db.SaveChanges();
    }


    public List<Gasto> ObtenerGastosPorConsorcio(int idConsorcio)
    {
        return _db.Gastos
             .Include(g => g.TipoGasto)
             .Where(g => g.IdConsorcio == idConsorcio)
             .OrderByDescending(g => g.FechaGasto)
             .ToList();
    }

    public Gasto ObtenerPorId(int id)
    {
        return _db.Gastos
                       .Include(g => g.TipoGasto)
                       .FirstOrDefault(g => g.Id == id);
    }

    public List<TipoGasto> ObtenerTiposGasto()
    {
        return _db.TiposGasto.ToList();
    }
}
