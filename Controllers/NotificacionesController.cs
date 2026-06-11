using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones;
using PracticaParcial.Models.Notificaciones.DTO;
using PracticaParcial.shared;
using System.Security.Claims;

namespace PracticaParcial.Controllers
{
    public class NotificacionesController : Controller
    {

        private readonly INotificacionesLogica _notificacionesLogica;
        private readonly IConsorcioService _consorcioService;

        public NotificacionesController(INotificacionesLogica notificacionesLogica, IConsorcioService consorcioService)
        {
            _notificacionesLogica = notificacionesLogica;
            _consorcioService = consorcioService;
        }

        public async Task<IActionResult> Index(int Id)
        {
            Consorcio? buscado = await obtenerConsorciosCorrespondientesAlIdDeUsuario(Id);

            if (buscado == null)
            {
                return RedirectToAction("Index", "Consorcio");
            }

            List<Notificacion> notificaciones = _notificacionesLogica.ObtenerNotificaciones(buscado.Id);

            ViewBag.ConsorcioNombre = buscado.Nombre;
            ViewBag.ConsorcioId = buscado.Id;

            return View(notificaciones);
        }

        private async Task<Consorcio> obtenerConsorciosCorrespondientesAlIdDeUsuario(int idConsorcio)
        {
            Guid userId = ClaimsExtension.GetUserId(User);

            Consorcio? buscado = await _consorcioService.ObtenerConsorcioPorId(idConsorcio, userId);

            return buscado;
        }

        public async Task<IActionResult> Agregar(int Id)
        {
            Consorcio? buscado = await obtenerConsorciosCorrespondientesAlIdDeUsuario(Id);

            if (buscado == null)
            {
                return RedirectToAction("Index", "Consorcio");
            }

            CrearNotificacionViewModel noti = new CrearNotificacionViewModel()
            {
                IdConsorcio = buscado.Id
            };

            ViewBag.ConsorcioNombre = buscado.Nombre;

            return View(noti);
        }
    }
}
