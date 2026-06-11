using PracticaParcial.Models.Consorcios;
using PracticaParcial.Persistence;

namespace PracticaParcial.Models.Notificaciones
{
    public interface INotificacionesLogica{
        List<Notificacion> ObtenerNotificaciones();

        //TODO metodo de improvisacion
        Consorcio ObtenerConsoricioProvisorio();
    }

    public class NotificacionesLogica : INotificacionesLogica{

        private readonly UnidadDbContext db;

        public NotificacionesLogica(UnidadDbContext db){
            this.db = db;
        }

        public List<Notificacion> ObtenerNotificaciones()
        {
            Consorcio consorcio=new Consorcio()
            {
                Id = 1,
                Nombre = "Gurren",
                Calle = "test",
                Ciudad = "test",
                Provincia = "test",
                CodigoPostal = "test",
                DiaVencimientoExpensas = 3,
                Latitud = 3,
                Longitud = 3,
                FechaCreacion = DateTime.Now,
                UserId = Guid.NewGuid()
            };

            Notificacion noti = new Notificacion()
            {
                Id = 1,
                consorcio = consorcio,
                Titulo = "First",
                Descripcion = "Llegue sano y a salvo",
                FechaDeCreacion = DateOnly.FromDateTime(DateTime.Now),
                FechaDeEnvio = null
            };

            Notificacion noti2 = new Notificacion()
            {
                Id = 1,
                consorcio = consorcio,
                Titulo = "Second",
                Descripcion = "Llegue sano y a salvo",
                FechaDeCreacion = DateOnly.FromDateTime(DateTime.Now),
                FechaDeEnvio = new DateOnly(2026, 8, 20)
            };

            List<Notificacion> notificaciones = new ();
            notificaciones.Add(noti);
            notificaciones.Add(noti2);

            return notificaciones;
        }

        public Consorcio ObtenerConsoricioProvisorio()
        {
            return new Consorcio()
            {
                Id = 1,
                Nombre = "Gurren",
                Calle = "test",
                Ciudad = "test",
                Provincia = "test",
                CodigoPostal = "test",
                DiaVencimientoExpensas = 3,
                Latitud = 3,
                Longitud = 3,
                FechaCreacion = DateTime.Now,
                UserId = Guid.NewGuid()
            };
        }
    }
}
