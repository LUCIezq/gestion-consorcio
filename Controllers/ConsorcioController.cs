using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcio;

namespace PracticaParcial.Controllers
{
    [Authorize]
    public class ConsorcioController : Controller
    {

        public ConsorcioController()
        {
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateConsorcioViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Create", "Consorcio");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}