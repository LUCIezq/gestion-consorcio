using Consorcio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticaParcial.Controllers
{
    public class UnidadesController : Controller
    {
        // GET: UnidadesController
        public ActionResult Index()
        {
            var db = new UnidadDbContext();
            var unidades = db.Unidades.ToList();
            
            return View(unidades);
        }

        // GET: UnidadesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UnidadesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnidadesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UnidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UnidadesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UnidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UnidadesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
