using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PracticaParcial.Controllers
{
    [Authorize]
    [Route("consorcio")]
    public class ConsorcioController : Controller
    {

        public ConsorcioController()
        {
        }

        [HttpGet]
        [Route("nuevo-consorcio")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Route("consorcios")]
        public IActionResult Index()
        {
            return View();
        }
    }
}