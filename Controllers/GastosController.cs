
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Gastos;
using PracticaParcial.Models.Gastos.DTos;
using PracticaParcial.shared;



namespace PracticaParcial.Controllers
{

    [Authorize]
    public class GastosController : Controller
    {
        private readonly IGastosService _gastosService;

        private readonly IGuardarArchivoService _guardarArchivoLogica;

        private readonly IConsorcioService _consorcioService;

        public GastosController(IGastosService gastosLogica, IGuardarArchivoService guardarArchivoLogica, IConsorcioService consorcioService)
        {
            _gastosService = gastosLogica;
            _guardarArchivoLogica = guardarArchivoLogica;
            _consorcioService = consorcioService;
        }

        public async Task<IActionResult> Agregar(int id)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(id, userId);

            if (consorcio == null)
            {
                return RedirectToAction("Index", "Consorcio");
            }

            ViewBag.Consorcio = consorcio!.Nombre;
            ViewBag.TiposGasto = _gastosService.ObtenerTiposGasto();
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
                ViewBag.TiposGasto = _gastosService.ObtenerTiposGasto();
                ViewBag.IdConsorcio = gasto.IdConsorcio;
                return View("Agregar", gasto);
            }

            var archivo = gasto.ArchivoComprobante;

            if (archivo != null && archivo.Length > 0)
            {
                gasto.ArchivoComprobanteGuardado = _guardarArchivoLogica.GuardarArchivo(archivo);
            }

            var nuevoGasto = gasto.ToEntity();
            _gastosService.AgregarGasto(nuevoGasto);
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

        public async Task<IActionResult> VerGastos(int id)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            Consorcio? consorcio = await _consorcioService.ObtenerConsorcioPorId(id, userId);

            if (consorcio == null)
            {
                return RedirectToAction("Index", "Consorcio");
            }

            ViewBag.IdConsorcio = id;
            ViewBag.ConsorcioNombre = consorcio.Nombre;

            var gastos = _gastosService.ObtenerGastosPorConsorcio(id);

            return View(gastos);
        }


        [HttpGet]
        [Route("Gastos/Editar/{idConsorcio}/{idGasto}")]
        public IActionResult Editar(int idConsorcio, int idGasto)
        {
            var gasto = _gastosService.ObtenerGasto(idGasto);

            if (gasto == null)
            {
                return RedirectToAction("VerGastos", new { id = idConsorcio });
            }

            ViewBag.TiposGasto = _gastosService.ObtenerTiposGasto();
            ViewBag.TiposGasto = _gastosService.ObtenerTiposGasto();
            return View(gasto);

        }


        [HttpPost]
        public IActionResult Editar(GastoViewModel gastoVM)
        {
            ModelState.Remove("ArchivoComprobante");
            if (!ModelState.IsValid)
            {
                ViewBag.TiposGasto = _gastosService.ObtenerTiposGasto();
                return View("Editar", gastoVM);
            }
            var gasto = gastoVM.ToEntity();


            if (gastoVM.ArchivoComprobante != null && gastoVM.ArchivoComprobante.Length > 0)
            {
                gastoVM.ArchivoComprobanteGuardado = _guardarArchivoLogica
                    .GuardarArchivo(gastoVM.ArchivoComprobante);
            }

            _gastosService.ActualizarGasto(gastoVM);

            return RedirectToAction("VerGastos", new { id = gastoVM.IdConsorcio });

        }


        [HttpPost]
        public IActionResult Eliminar(int id, int idConsorcio)
        {

            this.BorrarComprobante(id);

            _gastosService.EliminarGasto(id);

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
            var gastoVM = _gastosService.ObtenerGasto(id);

            if (gastoVM != null && !string.IsNullOrEmpty(gastoVM.ArchivoComprobanteGuardado))
            {
                var rutaArchivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comprobantes", gastoVM.ArchivoComprobanteGuardado);

                System.IO.File.Delete(rutaArchivo);
            }
        }
    }
}
