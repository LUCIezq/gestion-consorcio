
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Gastos;
using PracticaParcial.Models.Gastos.DTos;
using static System.Runtime.InteropServices.JavaScript.JSType;



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

        public IActionResult Agregar(int id)
        {

            ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
            ViewBag.IdConsorcio = id;
            var model = new GastoViewModel
            {
                IdConsorcio = id,
                FechaGasto = DateOnly.FromDateTime(DateTime.Now),
                AnioExpensa = DateTime.Now.Year,
                MesExpensa = DateTime.Now.Month
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult AgregarGasto(GastoViewModel gasto, string accion)
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

                gasto.ArchivoComprobanteGuardado = _guardarArchivoLogica.GuardarArchivo(archivo);
            }

            var nuevoGasto = gasto.ToEntity();
            _gastosLogica.AgregarGasto(nuevoGasto);
            ViewBag.gastoCreado = true;

            if (accion == "CrearOtroGasto")
            {
                TempData["MensajeExitoso"] = $"¡Gasto '{nuevoGasto.Nombre}' creado con éxito!";
            }

            return accion switch
            {
                "Guardar" => RedirectToAction("VerGastos", new { id = nuevoGasto.IdConsorcio }),
                "CrearOtroGasto" => RedirectToAction("Agregar", new { id = nuevoGasto.IdConsorcio }),
                _ => RedirectToAction("VerGastos", new { id = nuevoGasto.IdConsorcio })
            };

        }


        public IActionResult VerGastos(int id)
        {
            ViewBag.IdConsorcio = id;

            var gastos = _gastosLogica.ObtenerGastosPorConsorcio(id);

            return View(gastos);
        }


        [HttpGet]
        [Route("Gastos/Editar/{idConsorcio}/{idGasto}")]
        public IActionResult Editar(int idConsorcio, int idGasto)
        {
            var gasto = _gastosLogica.ObtenerGasto(idGasto);

            if (gasto == null)
            {
                return RedirectToAction("VerGastos", new { id = idConsorcio });
            }

            ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
            ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
            return View(gasto);

        }


        [HttpPost]
        public IActionResult Editar(GastoViewModel gastoVM)
        {
            ModelState.Remove("ArchivoComprobante");
            if (!ModelState.IsValid)
            {
                ViewBag.TiposGasto = _gastosLogica.ObtenerTiposGasto();
                return View("Editar", gastoVM);
            }


            if (gastoVM.ArchivoComprobante != null && gastoVM.ArchivoComprobante.Length > 0)
            {
                gastoVM.ArchivoComprobanteGuardado = _guardarArchivoLogica
                    .GuardarArchivo(gastoVM.ArchivoComprobante);
            }

            _gastosLogica.ActualizarGasto(gastoVM);

            return RedirectToAction("VerGastos", new { id = gastoVM.IdConsorcio });

        }


        [HttpPost]
        public IActionResult Eliminar(int id, int idConsorcio)
        {

            this.BorrarComprobante(id);

            _gastosLogica.EliminarGasto(id);

            return RedirectToAction("VerGastos", new { id = idConsorcio });
        }

        public IActionResult DescargarComprobante(string archivo)
        {
            var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comprobantes", archivo);

            var bytes = System.IO.File.ReadAllBytes(ruta);
            var extension = Path.GetExtension(archivo).ToLower();

            var contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                _ => "application/octet-stream"
            };

            return File(bytes, contentType, archivo);
        }

        private void BorrarComprobante(int id)
        {
            var gastoVM = _gastosLogica.ObtenerGasto(id);

            if (gastoVM != null && !string.IsNullOrEmpty(gastoVM.ArchivoComprobanteGuardado))
            {
                var rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comprobantes", gastoVM.ArchivoComprobanteGuardado);

                System.IO.File.Delete(rutaArchivo);
            }
        }
    }
}
