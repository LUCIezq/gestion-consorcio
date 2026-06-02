using Consorcio.Entidades;
using Logica;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Unidades;

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
            return View(unidades);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(UnidadViewModel unidadVM, string accionBoton)
        {
            if (!ModelState.IsValid)
                return View(unidadVM);

            unidadLogica.AgregarUnidad(unidadVM.ToEntity());

            if (accionBoton == "guardar_nuevo")
            {
                TempData["MensajeExito"] = $"Unidad {unidadVM.Nombre} creada con éxito";
                ModelState.Clear(); 
                return View(new UnidadViewModel());
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
    }
}
