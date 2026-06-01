using Consorcio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace UnidadLogica;

public interface IReservaLogica
{
    List<ReservaSUM> ObtenerReservas();
    void AgregarReserva(ReservaSUM reserva);
    void EliminarReserva(int id);
    ReservaSUM ObtenerPorId(int id);
    void ActualizarReserva(ReservaSUM reserva);
}

public class ReservaLogica : IReservaLogica
{
    private readonly UnidadDbContext db;
    public ReservaLogica(UnidadDbContext db)
    {
        this.db = db;
    }
    public List<ReservaSUM> ObtenerReservas()
    {
        return db.ReservasSUM
            .Include(r => r.Unidad)
            .OrderBy(r => r.Fecha)
            .ToList();
    }
    public void AgregarReserva(ReservaSUM reserva)
    {
        bool existe = db.ReservasSUM
            .Any(r => r.Fecha == reserva.Fecha && r.Turno == reserva.Turno);

        if (existe)
        {
            throw new Exception("Ya existe una reserva para ese turno.");
        }

        db.ReservasSUM.Add(reserva);
        db.SaveChanges();
    }
    public void EliminarReserva(int id)
    {
        var reserva = db.ReservasSUM.Find(id);
        if (reserva != null)
        {
            db.ReservasSUM.Remove(reserva);
            db.SaveChanges();
        }
    }
    public ReservaSUM ObtenerPorId(int id)
    {
        return db.ReservasSUM
            .Include (r => r.Unidad)
            .FirstOrDefault(r => r.Id == id);
    }
    public void ActualizarReserva(ReservaSUM reserva)
    {
        bool existe = db.ReservasSUM
            .Any(r => r.Id != reserva.Id && r.Fecha == reserva.Fecha && r.Turno == reserva.Turno);
        
        if (existe)
        {
            throw new Exception("Ya existe una reserva para ese turno.");
        }

        db.ReservasSUM.Update(reserva);
        db.SaveChanges();
    }
}