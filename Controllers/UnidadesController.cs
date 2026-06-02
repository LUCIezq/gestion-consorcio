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
        public IActionResult Agregar(UnidadViewModel unidadVM)
        {
            if (!ModelState.IsValid)
                return View(unidadVM);

            unidadLogica.AgregarUnidad(unidadVM.ToEntity());
            return RedirectToAction("Index");
        }
    
        public IActionResult Eliminar(int id)
        {
            unidadLogica.EliminarUnidad(id);
            return RedirectToAction("Index");
        }
    }
}
