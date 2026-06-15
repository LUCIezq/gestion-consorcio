using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Expensas;
using PracticaParcial.Models.Expensas.DTO;

namespace PracticaParcial.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensasApiController : ControllerBase
    {
        private readonly IExpensasService _service;

        public ExpensasApiController(IExpensasService service)
        {
            _service = service;
        }

        // GET api/<ExpensasApiController>/5
        [HttpGet("{consorcioId}")]
        public async Task<ActionResult<List<ExpensasViewModel>>> Get(int consorcioId)
        {
            return Ok(await _service.ObtenerExpensasAsync(consorcioId));
        }
    }
}
