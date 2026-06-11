using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones;

namespace PracticaParcial.Controllers
{
    public class NotificacionesController : Controller{

        public IActionResult Index()
        {
            //TODO Notificacion y consorcio Hardcodeado
            Consorcio consorcio = new Consorcio(){
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
                FechaDeEnvio = new DateOnly(2026,8,20)
            };

            //TODO debe haber un metodo que me traiga todos las notificaciones
            List<Notificacion> notificaciones= new List<Notificacion>();
            notificaciones.Add(noti);
            notificaciones.Add(noti2);


            //TODO buscar el consorcio y setear los valores de NOMBRE y ID en viewBag
            ViewBag.ConsorcioNombre = consorcio.Nombre;
            ViewBag.ConsorcioId= consorcio.Id;

            return View(notificaciones);
        }
    }
}
