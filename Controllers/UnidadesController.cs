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
        private readonly IUnidadesLogica _unidadLogica;

        private readonly IConsorcioService _consorcioService;

        public UnidadesController(IUnidadesLogica unidadLogica, IConsorcioService consorcioService)
        {
            this._unidadLogica = unidadLogica;
            this._consorcioService = consorcioService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(id, userId);

            if (consorcio == null)
            {
                //TempData["ErrorMessage"] = "No se encontró el consorcio.";
                return RedirectToAction("Index", "Consorcio");
            }

            var unidadesBd = _unidadLogica.ObtenerUnidadesPorConsorcio(id);

            var viewModels = unidadesBd
                .Select(UnidadViewModel.FromEntity)
                .ToList();

            ViewBag.ConsorcioId = id;
            ViewBag.ConsorcioNombre = consorcio?.Nombre ?? "Consorcio Desconocido";

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Agregar(int id)
        {
            int consorcioId = id;
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(id, userId);

            if (consorcio == null)
            {
                //-> Podriamos mandar el id a Unidades
                return RedirectToAction("Index", "Consorcio");
            }

            var viewModel = new UnidadViewModel
            {
                IdConsorcio = id,
                NombreConsorcio = consorcio != null ? consorcio.Nombre : "Consorcio Desconocido"
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(UnidadViewModel unidadVM, string accionBoton)
        {
            if (!ModelState.IsValid)
                return View(unidadVM);

            var nuevaUnidad = unidadVM.ToEntity();
            nuevaUnidad.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);

            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcioDb = await _consorcioService.ObtenerConsorcioPorId(unidadVM.IdConsorcio, userId);

            nuevaUnidad.Consorcio = consorcioDb;

            _unidadLogica.AgregarUnidad(nuevaUnidad);

            if (accionBoton == "guardar_nuevo")
            {
                TempData["MensajeExito"] = $"Unidad {unidadVM.Nombre} creada con éxito";
                ModelState.Clear();

                return View(new UnidadViewModel { IdConsorcio = unidadVM.IdConsorcio });
            }

            return RedirectToAction("Index", new { id = unidadVM.IdConsorcio });
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            Guid userId = ClaimsExtension.GetUserId(User);
            var unidad = _unidadLogica.ObtenerUnidadPorId(id, userId);

            if (unidad == null)
                return RedirectToAction("Index", "Consorcio");
            var viewModel = UnidadViewModel.FromEntity(unidad);

            return View(viewModel);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            await _unidadLogica.EliminarUnidad(id, userId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            Guid userId = ClaimsExtension.GetUserId(User);
            var unidad = _unidadLogica.ObtenerUnidadPorId(id, userId);

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
            Guid userId = ClaimsExtension.GetUserId(User);
            var unidadExistente = _unidadLogica.ObtenerUnidadPorId(unidadVM.IdUnidad, userId);
            if (unidadExistente == null)
                return NotFound();
            unidadExistente.Nombre = unidadVM.Nombre;
            unidadExistente.NombrePropietario = unidadVM.NombrePropietario;
            unidadExistente.ApellidoPropietario = unidadVM.ApellidoPropietario;
            unidadExistente.EmailPropietario = unidadVM.EmailPropietario;
            unidadExistente.Superficie = unidadVM.Superficie.Value;
            _unidadLogica.ActualizarUnidad(unidadExistente);
            return RedirectToAction("Index");
        }
    }
}
