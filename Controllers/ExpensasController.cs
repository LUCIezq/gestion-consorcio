using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Expensas;
using PracticaParcial.Models.Expensas.DTO;
using PracticaParcial.Models.Unidades.DTOs;
using PracticaParcial.shared;

namespace PracticaParcial.Controllers
{
    public class ExpensasController : Controller
    {
        private readonly IExpensasService _expensasService;
        private readonly IConsorcioService _consorcioService;

        public ExpensasController(IExpensasService expensasService, IConsorcioService consorcioService)
        {
            this._expensasService = expensasService;
            this._consorcioService = consorcioService;
        }
        public async Task<IActionResult> VerExpensas(int id)
        {
            int consorcioId = id;
            Guid userId = ClaimsExtension.GetUserId(User);
            var consorcio = await _consorcioService.ObtenerConsorcioPorId(consorcioId, userId);

            if (consorcio == null)
            {
                return RedirectToAction("Index", "Consorcio");
            }

            var model = new VerExpensasViewModel
            {
                Resumen = await this._expensasService.ObtenerResumenMesActualAsync(consorcioId),
                Expensas = await this._expensasService.ObtenerExpensasAsync(consorcioId),
                NombreConsorcio = consorcio.Nombre
            };

            return View(model);
        }
    }
}
