using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Reserva;
using PracticaParcial.Models.Unidades;
using PracticaParcial.shared;

namespace PracticaParcial.Controllers
{
    [Authorize]
    public class ReservasSUMController : Controller
    {
        private readonly IReservaLogica _reservaLogica;
        private readonly IUnidadesLogica _unidadesLogica;
        private readonly IConsorcioService _consorcioService;
        public ReservasSUMController(IReservaLogica reservaLogica, IUnidadesLogica unidadesLogica, IConsorcioService consorcioService)
        {
            _reservaLogica = reservaLogica;
            _unidadesLogica = unidadesLogica;
            _consorcioService = consorcioService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int consorcioId)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            ViewBag.NombreConsorcio = consorcio != null ? consorcio.Nombre : "Desconocido";
            ViewBag.ConsorcioId = consorcioId;
            return View(_reservaLogica.ObtenerReservas(consorcioId));
        }

        [HttpGet]
        public async Task<IActionResult> Agregar(int consorcioId)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            ViewBag.NombreConsorcio = consorcio != null ? consorcio.Nombre : "Desconocido";
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);
            ViewBag.ConsorcioId = consorcioId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ReservaSUM reserva, int consorcioId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _reservaLogica.AgregarReserva(reserva);
                    return RedirectToAction("Index", new { consorcioId = consorcioId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            ViewBag.NombreConsorcio = consorcio != null ? consorcio.Nombre : "Desconocido";
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);
            ViewBag.ConsorcioId = consorcioId;
            return View(reserva);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id, int consorcioId)
        {
            var reserva = _reservaLogica.ObtenerPorId(id);
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            if (reserva == null)
                return NotFound();

            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);
            ViewBag.NombreConsorcio = consorcio != null ? consorcio.Nombre : "Desconocido";
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);

            ViewBag.ConsorcioId = consorcioId;

            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ReservaSUM reserva, int consorcioId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _reservaLogica.ActualizarReserva(reserva);

                    return RedirectToAction("Index", new { consorcioId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);
            ViewBag.ConsorcioId = consorcioId;

            return View(reserva);
        }

        public async Task<IActionResult> Eliminar(int id, int consorcioId)
        {
            _reservaLogica.EliminarReserva(id);
            return RedirectToAction("Index", new { consorcioId = consorcioId });
        }
    }
}
