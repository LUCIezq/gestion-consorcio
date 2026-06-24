using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Expensas;
using PracticaParcial.Models.Expensas.DTO;
using PracticaParcial.shared;

namespace PracticaParcial.Controllers;

[Authorize]
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
            ConsorcioId = consorcioId,
            Resumen = await this._expensasService.ObtenerResumenMesActualAsync(consorcioId),
            NombreConsorcio = consorcio.Nombre
        };

        return View(model);
    }
}
