using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones.DTO;
using PracticaParcial.Persistence;
using System.Diagnostics;

namespace PracticaParcial.Models.Notificaciones
{
    public interface INotificacionesLogica{
        List<Notificacion> ObtenerNotificaciones(int IdConsorcio);
        void AgregarNotificacion(Notificacion nueva);

        void EnviarNotificacion(Notificacion notificacion);

        Notificacion ObtenerNotificacionPorId(int idNotificacion);
        void EliminarNotificacion(Notificacion notificacion);
        void ActualizarNotificacion(Notificacion notificacion, EditarNotificacionViewModel notiModel);
    }

    public class NotificacionesLogica : INotificacionesLogica{

        private readonly UnidadDbContext db;

        public NotificacionesLogica(UnidadDbContext db){
            this.db = db;
        }

        public List<Notificacion> ObtenerNotificaciones(int IdConsorcio)
        {

            List<Notificacion> notificaciones = db.Notificaciones.
                                                Where(n => n.consorcio.Id == IdConsorcio)
                                                .ToList(); ;

            return notificaciones;
        }

        public void AgregarNotificacion(Notificacion nueva)
        {
            db.Notificaciones.Add(nueva);
            db.SaveChanges();
        }

        public void EnviarNotificacion(Notificacion notificacion)
        {
            //TODO: Aca agregar el ENVIO de mail

            notificacion.FechaDeEnvio = DateOnly.FromDateTime(DateTime.Now);
            db.Notificaciones.Update(notificacion);
            db.SaveChanges();
        }

        public Notificacion ObtenerNotificacionPorId(int idNotificacion)
        {
            return db.Notificaciones
                .Include(n=> n.consorcio)
                .FirstOrDefault(n => n.Id == idNotificacion);
        }

        public void EliminarNotificacion(Notificacion notificacion)
        {
            db.Notificaciones.Remove(notificacion);
            db.SaveChanges();
        }

        public void ActualizarNotificacion(Notificacion notificacion, EditarNotificacionViewModel notiModel)
        {

            notificacion.Titulo = notiModel.Titulo;
            notificacion.Descripcion = notiModel.Descripcion;
            db.Notificaciones.Update(notificacion);
            db.SaveChanges();
        }
    }
}
