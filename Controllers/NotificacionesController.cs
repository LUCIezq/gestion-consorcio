using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Notificaciones;
using PracticaParcial.Models.Notificaciones.DTO;
using PracticaParcial.shared;
using System.Diagnostics;
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

            List<MostrarNotificacionesViewModel> list = MostrarNotificacionesViewModel.ObtenerListaDeViewModel(notificaciones);

            ViewBag.ConsorcioNombre = buscado.Nombre;
            ViewBag.ConsorcioId = buscado.Id;

            return View(list);
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

        [HttpPost]
        public async Task<IActionResult> Agregar(CrearNotificacionViewModel notificacion)
        {

            if (!ModelState.IsValid)
            {
                return View(notificacion);
            }

            Notificacion nueva = notificacion.toEntity();
            nueva.FechaDeCreacion = DateOnly.FromDateTime(DateTime.Now);
            nueva.consorcio = await obtenerConsorciosCorrespondientesAlIdDeUsuario(notificacion.IdConsorcio);

            _notificacionesLogica.AgregarNotificacion(nueva);

            if (Request.Form["accion"] == "Enviar")
            {
                await _notificacionesLogica.EnviarNotificacion(nueva);
            }


            return RedirectToAction("Index", new { id = notificacion.IdConsorcio });
        }

        public IActionResult Eliminar(int id)
        {
            Notificacion noti = _notificacionesLogica.ObtenerNotificacionPorId(id);
            int IdConsorcio = noti.consorcio.Id;

            if (noti.FechaDeEnvio == null)
            {
                _notificacionesLogica.EliminarNotificacion(noti);
            }

            return RedirectToAction("Index", new { id = IdConsorcio }); ;
        }

        public async Task<IActionResult> Enviar(int id)
        {
            Notificacion noti = _notificacionesLogica.ObtenerNotificacionPorId(id);
            int IdConsorcio = noti.consorcio.Id;

            if (noti.FechaDeEnvio == null)
            {
                await _notificacionesLogica.EnviarNotificacion(noti);
            }

            return RedirectToAction("Index", new { id = IdConsorcio }); ;
        }

        public IActionResult Editar(int idNotificacion)
        {
            Notificacion noti = _notificacionesLogica.ObtenerNotificacionPorId(idNotificacion);

            EditarNotificacionViewModel notiModel = EditarNotificacionViewModel.toViewModel(noti);

            ViewBag.ConsorcioNombre = noti.consorcio.Nombre;

            return View(notiModel);
        }

        [HttpPost]
        public IActionResult Editar(EditarNotificacionViewModel notiModel)
        {
            Notificacion noti = _notificacionesLogica.ObtenerNotificacionPorId(notiModel.IdNotificacion);

            _notificacionesLogica.ActualizarNotificacion(noti, notiModel);

            return RedirectToAction("Index", new { id = notiModel.IdConsorcio }); ;
        }

        public IActionResult VerDetalle(int Id)
        {
            Notificacion noti = _notificacionesLogica.ObtenerNotificacionPorId(Id);

            return View(noti);
        }
    }
}
