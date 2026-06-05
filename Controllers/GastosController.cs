
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Gastos;
using PracticaParcial.Models.Gastos.DTos;



namespace PracticaParcial.Controllers
{
    public class GastosController : Controller
    {
        private readonly IGastosLogica _gastosLogica;

        private readonly IGuardarArchivoLogica _guardarArchivoLogica;

        public GastosController(IGastosLogica gastosLogica, IGuardarArchivoLogica guardarArchivoLogica)
        {
            _gastosLogica = gastosLogica;
            _guardarArchivoLogica = guardarArchivoLogica;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Agregar() {
            ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
            ViewBag.IdConsorcio = 1;  
            return View(); 
        }


        [HttpPost]
        public async Task<IActionResult> AgregarGasto(GastoViewModel gasto)
        {
            try
            {
                ModelState.Remove("ArchivoComprobante");

                if (!ModelState.IsValid)
                {
                    ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
                    ViewBag.IdConsorcio = gasto.IdConsorcio;
                    return View("Agregar", gasto);
                }

                var archivo = gasto.ArchivoComprobante;

                if (archivo != null && archivo.Length > 0)
                {
                    var extension = Path.GetExtension(archivo.FileName).ToLower();
                    var extensionesPermitidas = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".docx" };

                    if (!extensionesPermitidas.Contains(extension))
                    {
                        ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
                        ViewBag.IdConsorcio = gasto.IdConsorcio;
                        ModelState.AddModelError("", "Formato no válido.");
                        return View("Agregar", gasto);
                    }

                    gasto.ArchivoComprobanteGuardado = await _guardarArchivoLogica.GuardarArchivoAsync(archivo);
                }

                var nuevoGasto = gasto.ToEntity();
                _gastosLogica.AgregarGasto(nuevoGasto);

                return RedirectToAction("VerGastos", new { id = nuevoGasto.IdConsorcio });
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }



        public IActionResult VerGastos(int id)
        {
            ViewBag.IdConsorcio = id; 

            var gastos = _gastosLogica.ObtenerGastosPorConsorcio(id);

            return View(gastos);
        }
    }
}
