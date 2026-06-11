using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Expensas;
using PracticaParcial.Persistence.Expensas;

namespace PracticaParcial.Controllers
{
    [ApiController]
    [Route("api/expensas")]
    public class ExpensasController : Controller
    {
        private readonly IExpensasService _expensasService;
        public ExpensasController(IExpensasService expensasService)
        {
            this._expensasService = expensasService;
        }
        [HttpGet("{consorcioId}")]
        public IActionResult VerExpensas(int consorcioId)
        {
            return Ok(_expensasService.ObtenerExpensas(consorcioId));
        }
    }
}
