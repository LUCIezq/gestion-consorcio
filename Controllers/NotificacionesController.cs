using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones;

namespace PracticaParcial.Controllers
{
    public class NotificacionesController : Controller{

        private readonly INotificacionesLogica _notificacionesLogica;
        private readonly IConsorcioService _consorcioService;

        public NotificacionesController(INotificacionesLogica notificacionesLogica, IConsorcioService consorcioService)
        {
            _notificacionesLogica = notificacionesLogica;
            _consorcioService = consorcioService;
        }

        public IActionResult Index(int Id)
        {
            int IdConsorcio = Id;
            
            List<Notificacion> notificaciones = _notificacionesLogica.ObtenerNotificaciones(IdConsorcio);

            //TODO buscar el consorcio y setear los valores de NOMBRE y ID en viewBag
            //Consorcio consorcio = _consorcioService.ObtenerConsorcioPorId(IdConsorcio);
            Consorcio consorcio = _notificacionesLogica.ObtenerConsorcioProvisorio();
            ViewBag.ConsorcioNombre = consorcio.Nombre;
            ViewBag.ConsorcioId= consorcio.Id;

            return View(notificaciones);
        }
    }
}
