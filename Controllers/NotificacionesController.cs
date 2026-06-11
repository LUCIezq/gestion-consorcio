using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones;

namespace PracticaParcial.Controllers
{
    public class NotificacionesController : Controller{

        private readonly INotificacionesLogica _notificacionesLogica;
        private readonly IConsorcioService _consorcioService;
        //servicio para buscar los consorcios

        //public NotificacionesController(INotificacionesLogica notificacionesLogica, IConsorcioService consorcioService)
        public NotificacionesController(INotificacionesLogica notificacionesLogica)
        {
            _notificacionesLogica = notificacionesLogica;
            //_consorcioService = consorcioService;
        }

        public IActionResult Index()
        {
            List<Notificacion> notificaciones= _notificacionesLogica.ObtenerNotificaciones();

            //TODO buscar el consorcio y setear los valores de NOMBRE y ID en viewBag
            Consorcio consorcio = _notificacionesLogica.ObtenerConsoricioProvisorio();
            ViewBag.ConsorcioNombre = consorcio.Nombre;
            ViewBag.ConsorcioId= consorcio.Id;

            return View(notificaciones);
        }
    }
}
