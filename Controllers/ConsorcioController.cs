using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.shared;

namespace PracticaParcial.Controllers
{
    [Authorize]
    public class ConsorcioController : Controller
    {
        private readonly IConsorcioService _consorcioService;

        public ConsorcioController(IConsorcioService consorcioService)
        {
            _consorcioService = consorcioService;
        }

        [HttpGet]
        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar(CreateConsorcioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Guid userId = ClaimsExtension.GetUserId(User);

            GuardarConsorcioResponse response = await _consorcioService.GuardarConsorcio(model, userId);

            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(model);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Index", "Consorcio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarYCrearOtro(CreateConsorcioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Guardar", model);
            }

            Guid userId = ClaimsExtension.GetUserId(User);

            GuardarConsorcioResponse response = await _consorcioService.GuardarConsorcio(model, userId);

            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View("Guardar", model);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Guardar", "Consorcio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarYCrearUnidad(CreateConsorcioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Guardar", model);
            }

            Guid userId = ClaimsExtension.GetUserId(User);

            GuardarConsorcioResponse response = await _consorcioService.GuardarConsorcio(model, userId);

            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View("Guardar", model);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Agregar", "Unidades", new { consorcioId = response.IdConsorcio });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            EliminarConsorcioResponse resultado = await _consorcioService.EliminarConsorcio(id);

            if (!resultado.Success)
            {
                ModelState.AddModelError(string.Empty, resultado.Message);
                return View("Index");
            }
            TempData["SuccessMessage"] = resultado.Message;
            return RedirectToAction("Index", "Consorcio");
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 5;
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcios = await _consorcioService.ObtenerConsorciosPaginados(userId, page, pageSize);
            return View(consorcios);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCoordenadas()
        {
            var resultado = await _consorcioService.ObtenerCoordenadas();

            return Ok(
                resultado
            );
        }
    }
}