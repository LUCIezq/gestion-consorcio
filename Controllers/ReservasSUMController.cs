using Consorcio.Entidades;
using Logica;
using Microsoft.AspNetCore.Mvc;
using UnidadLogica;

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
        public IActionResult Index()
        {
            return View(_reservaLogica.ObtenerReservas());
        }

        public IActionResult Agregar()
        {
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidades();
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(ReservaSUM reserva)
        {
            if (ModelState.IsValid)
            {
                _reservaLogica.AgregarReserva(reserva);
                return RedirectToAction("Index");
            }
            ViewBag.Unidades = _unidadesLogica.ObtenerUnidades();
            return View(reserva);
        }

        public IActionResult Eliminar(int id)
        {
            _reservaLogica.EliminarReserva(id);
            return RedirectToAction("Index");
        }
    }
}
