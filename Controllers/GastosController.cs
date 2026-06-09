using Microsoft.AspNetCore.Mvc;
using PracticaParcial.Models.Gastos;
using PracticaParcial.Models.Gastos.DTO;

namespace PracticaParcial.Controllers;

public class GastosController : Controller
{
    private readonly IGastoService _gastoService;
    public GastosController(IGastoService gastoService) 
    {
        this._gastoService = gastoService;
    }
    public IActionResult Index()
    {
        return View(_gastoService.ObtenerGastos());
    }

    [HttpGet]
    public IActionResult Agregar()
    {
        ViewBag.Gasto = _gastoService.ObtenerGastos();
        return View();
    }

    [HttpPost]
    public IActionResult Agregar(GastoViewModel gastoVM)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Gasto = this._gastoService.ObtenerGastos();
            return View(gastoVM);
        }
        var gasto = gastoVM.ToEntity();

        this._gastoService.Agregar(gasto);
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id)
    {
        _gastoService.Eliminar(id);
        return RedirectToAction("Index");
    }
}
