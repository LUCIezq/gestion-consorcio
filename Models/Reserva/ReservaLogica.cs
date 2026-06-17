using Microsoft.EntityFrameworkCore;
namespace PracticaParcial.Models.Reserva;

using PracticaParcial.Models.Consorcios;
using PracticaParcial.Persistence;
public interface IReservaLogica
{
    List<ReservaSUM> ObtenerReservas(int consorcioId);
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
    public List<ReservaSUM> ObtenerReservas(int consorcioId)
    {
        return db.ReservasSUM
        .Include(r => r.Unidad)
        .Where(r => r.Unidad.Consorcio.Id == consorcioId)
        .OrderBy(r => r.Fecha)
        .ToList();
    }
    public void AgregarReserva(ReservaSUM reserva)
    {
        var unidad = db.Unidades
        .Include(u => u.Consorcio)
        .FirstOrDefault(u => u.IdUnidad == reserva.UnidadId);

        if (unidad == null)
        {
            throw new Exception("La unidad no existe.");
        }

        int consorcioId = unidad.Consorcio.Id;

        bool existe = db.ReservasSUM
            .Include(r => r.Unidad)
            .ThenInclude(u => u.Consorcio)
            .Any(r =>
                r.Unidad.Consorcio.Id == consorcioId &&
                r.Fecha == reserva.Fecha &&
                r.Turno == reserva.Turno);

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
            .Include(r => r.Unidad)
            .FirstOrDefault(r => r.Id == id);
    }
    public void ActualizarReserva(ReservaSUM reserva)
    {
        var unidad = db.Unidades
            .Include(u => u.Consorcio)
            .FirstOrDefault(u => u.IdUnidad == reserva.UnidadId);

        int consorcioId = unidad.Consorcio.Id;

        bool existe = db.ReservasSUM
            .Include(r => r.Unidad)
            .ThenInclude(u => u.Consorcio)
            .Any(r =>
                r.Id != reserva.Id &&
                r.Unidad.Consorcio.Id == consorcioId &&
                r.Fecha == reserva.Fecha &&
                r.Turno == reserva.Turno);

        if (existe)
        {
            throw new Exception("Ya existe una reserva para ese turno.");
        }

        db.ReservasSUM.Update(reserva);
        db.SaveChanges();
    }
}