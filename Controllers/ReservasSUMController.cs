using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Reserva;
using PracticaParcial.Models.Unidades;

namespace PracticaParcial.Controllers
{
    public class ReservasSUMController : Controller
    {
        private readonly IReservaLogica _reservaLogica;
        private readonly IUnidadesLogica _unidadesLogica;
        public ReservasSUMController(IReservaLogica reservaLogica, IUnidadesLogica unidadesLogica)
        {
            _reservaLogica = reservaLogica;
            _unidadesLogica = unidadesLogica;
        }
        public IActionResult Index(int consorcioId)
        {
            ViewBag.ConsorcioId = consorcioId;
            return View(_reservaLogica.ObtenerReservas(consorcioId));
        }

        public IActionResult Agregar(int consorcioId)
        {
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);
            ViewBag.ConsorcioId = consorcioId;
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(ReservaSUM reserva, int consorcioId)
        {
            if (ModelState.IsValid)
            {
                _reservaLogica.AgregarReserva(reserva);
                return RedirectToAction("Index", new { consorcioId = consorcioId });
            }
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidadesPorConsorcio(consorcioId);
            ViewBag.ConsorcioId = consorcioId;
            return View(reserva);
        }

        public IActionResult Eliminar(int id, int consorcioId)
        {
            _reservaLogica.EliminarReserva(id);
            return RedirectToAction("Index", new { consorcioId = consorcioId });
        }
    }
}
