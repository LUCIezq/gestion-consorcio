using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcio;

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
        public IActionResult Guardar(CreateConsorcioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Guardar", "Consorcio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuardarYCrearOtro(CreateConsorcioViewModel model)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuardarYCrearUnidad(CreateConsorcioViewModel model)
        {

            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}