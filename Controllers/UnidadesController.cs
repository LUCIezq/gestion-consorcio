
using PracticaParcial.Models.Unidades;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Unidades.DTOs;
namespace PracticaParcial.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly IUnidadesLogica unidadLogica;


        public UnidadesController(IUnidadesLogica unidadLogica)
        {
            this.unidadLogica = unidadLogica;
        }

        public ActionResult Index()
        {
            var unidades = unidadLogica.ObtenerUnidades();

            var viewModels = unidades
                .Select(UnidadViewModel.FromEntity)
                .ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Agregar(int consorcioId)
        {
            var viewModel = new UnidadViewModel
            {
                IdConsorcio = consorcioId
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Agregar(UnidadViewModel unidadVM, string accionBoton)
        {
            if (!ModelState.IsValid)
                return View(unidadVM);

            var nuevaUnidad = unidadVM.ToEntity();
            nuevaUnidad.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);

            unidadLogica.AgregarUnidad(nuevaUnidad);

            if (accionBoton == "guardar_nuevo")
            {
                TempData["MensajeExito"] = $"Unidad {unidadVM.Nombre} creada con éxito";
                ModelState.Clear();

                return View(new UnidadViewModel { IdConsorcio = unidadVM.IdConsorcio });
            }

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            var unidad = unidadLogica.ObtenerUnidades().FirstOrDefault(u => u.IdUnidad == id);

            if (unidad == null)
                return RedirectToAction("Index");

            return View(unidad);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            unidadLogica.EliminarUnidad(id);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var unidad = unidadLogica.ObtenerUnidadPorId(id);
            if (unidad == null)
                return NotFound();

            return View(UnidadViewModel.FromEntity(unidad));
        }

        [HttpPost]
        public IActionResult Editar(UnidadViewModel unidadVM)
        {
            if (!ModelState.IsValid)
                return View(unidadVM);
            var unidadExistente = unidadLogica.ObtenerUnidadPorId(unidadVM.IdUnidad);
            if (unidadExistente == null)
                return NotFound();
            unidadExistente.Nombre = unidadVM.Nombre;
            unidadExistente.NombrePropietario = unidadVM.NombrePropietario;
            unidadExistente.ApellidoPropietario = unidadVM.ApellidoPropietario;
            unidadExistente.EmailPropietario = unidadVM.EmailPropietario;
            unidadExistente.Superficie = unidadVM.Superficie.Value;
            unidadLogica.ActualizarUnidad(unidadExistente);
            return RedirectToAction("Index");

        }
    }
}
