using Consorcio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logica;
using PracticaParcial.Models.Unidades;

namespace PracticaParcial.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly IUnidadLogica unidadLogica;


        public UnidadesController(IUnidadLogica unidadLogica)
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

    }
}
