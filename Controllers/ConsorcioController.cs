using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Consorcios.DTOs;

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

            GuardarConsorcioResponse response = await _consorcioService.GuardarConsorcio(model);

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

            GuardarConsorcioResponse response = await _consorcioService.GuardarConsorcio(model);

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

            GuardarConsorcioResponse response = await _consorcioService.GuardarConsorcio(model);

            if (!response.Success)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View("Guardar", model);
            }

            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction("Agregar", "Unidades", new { consorcioId = response.IdConsorcio });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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