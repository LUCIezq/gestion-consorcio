using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones;

namespace PracticaParcial.Controllers
{
    public class NotificacionesController : Controller{

        public IActionResult Index()
        {
            //TODO notificaiones Hardcodeadas
            Consorcio test = new Consorcio(){
                Id = 1,
                Nombre = "TEST",
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
                consorcio = test,
                Titulo = "First",
                Descripcion = "Llegue sano y a salvo",
                FechaDeCreacion = DateOnly.FromDateTime(DateTime.Now),
                FechaDeEnvio = null
            };

            Notificacion noti2 = new Notificacion()
            {
                Id = 1,
                consorcio = test,
                Titulo = "Second",
                Descripcion = "Llegue sano y a salvo",
                FechaDeCreacion = DateOnly.FromDateTime(DateTime.Now),
                FechaDeEnvio = new DateOnly(2026,8,20)
            };

            List<Notificacion> list = new List<Notificacion>();
            list.Add(noti);
            list.Add(noti2);

            return View(list);
        }
    }
}
