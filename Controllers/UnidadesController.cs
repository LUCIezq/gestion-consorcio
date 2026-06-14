using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Unidades;
using PracticaParcial.Models.Unidades.DTOs;
using PracticaParcial.Persistence.Consorcios;
using PracticaParcial.shared;
using System.Runtime.CompilerServices;
namespace PracticaParcial.Controllers
{

    [Authorize]
    public class UnidadesController : Controller
    {
        private readonly IUnidadesLogica unidadLogica;

        private readonly IConsorcioService _consorcioService;

        public UnidadesController(IUnidadesLogica unidadLogica, IConsorcioService consorcioService)
        {
            this.unidadLogica = unidadLogica;
            this._consorcioService = consorcioService;
        }

        [HttpGet]
        public ActionResult Index(int id, int pagina = 1)
        {
            int consorcioId = id;
            var resultado = unidadLogica.ObtenerUnidadesPorConsorcio(consorcioId, pagina);

            var viewModels = resultado.Unidades
                .Select(UnidadViewModel.FromEntity)
                .ToList();

            int cantidadPorPagina = 5;
            int totalPaginas = (int)Math.Ceiling((double)resultado.TotalRegistros / cantidadPorPagina);

            ViewBag.ConsorcioId = consorcioId;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar(int id)
        {
            int consorcioId = id;
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            var viewModel = new UnidadViewModel
            {
                IdConsorcio = consorcioId,
                NombreConsorcio = consorcio != null ? consorcio.Nombre : "Consorcio Desconocido"
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Agregar(UnidadViewModel unidadVM, string accionBoton)
        {
            if (!ModelState.IsValid)
                return View(unidadVM);

            bool yaExiste = await unidadLogica.ExisteUnidadEnConsorcio(unidadVM.Nombre, unidadVM.IdConsorcio);

            if (yaExiste)
            {
                ModelState.AddModelError("Nombre", $"La unidad '{unidadVM.Nombre}' ya está registrada en este consorcio.");
                return View(unidadVM);
            }

            var nuevaUnidad = unidadVM.ToEntity();
            nuevaUnidad.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);

            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcioDb = await _consorcioService.ObtenerConsorcioPorId(unidadVM.IdConsorcio, userId);

            nuevaUnidad.Consorcio = consorcioDb;

            unidadLogica.AgregarUnidad(nuevaUnidad);

            if (accionBoton == "guardar_nuevo")
            {
                TempData["MensajeExito"] = $"Unidad {unidadVM.Nombre} creada con éxito";
                ModelState.Clear();

                return View(new UnidadViewModel { IdConsorcio = unidadVM.IdConsorcio });
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var unidad = unidadLogica.ObtenerUnidadPorId(id);

            if (unidad == null)
                return NotFound();

            var viewModel = UnidadViewModel.FromEntity(unidad);

            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(unidad.Consorcio.Id, userId);
            viewModel.NombreConsorcio = consorcio != null ? consorcio.Nombre : "Desconocido";

            return View(viewModel);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            await _consorcioService.EliminarConsorcio(id, userId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var unidad = unidadLogica.ObtenerUnidadPorId(id);

            if (unidad == null)
                return NotFound();

            var viewModel = UnidadViewModel.FromEntity(unidad);

            viewModel.NombreConsorcio = unidad.Consorcio != null ? unidad.Consorcio.Nombre : "Consorcio Desconocido";

            viewModel.IdConsorcio = unidad.Consorcio != null ? unidad.Consorcio.Id : 0;

            return View(viewModel);
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
