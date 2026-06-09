using Microsoft.AspNetCore.Mvc;

namespace PracticaParcial.Controllers
{
    public class ExpensasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
