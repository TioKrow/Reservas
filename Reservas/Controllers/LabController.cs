using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;
using Reservas.Models.ViewModel;

namespace Reservas.Controllers
{
    public class LabController : Controller
    {
        private readonly DbReservasContext _context;

        public LabController(DbReservasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TbLabs.ToListAsync());
        }

        public IActionResult AgregarLab()
        {
            ViewData["Laboratorios"] = new SelectList(_context.TbLabs, "IdLab", "NombreLab");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarLab(LabViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lab = new TbLab()
                {
                    NombreLab = model.NombreLab,
                };
                _context.Add(lab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Laboratorios"] = new SelectList(_context.TbLabs, "IdLab", "NombreLab");

            return View();
        }
        public async Task<IActionResult> ModificarLab(int IdLab)
        {
            EditarLabViewModel model = new EditarLabViewModel();
            using (var db = new DbReservasContext())
            {
                var oLab = db.TbLabs.Find(IdLab);
                model.NombreLab = oLab.NombreLab;
                model.IdLab = oLab.IdLab;
            }
            ViewData["Laboratorios"] = new SelectList(_context.TbLabs, "IdLab", "NombreLab");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarLab(EditarLabViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new DbReservasContext())
            {
                var oLab = db.TbLabs.Find(model.IdLab);
                oLab.NombreLab = model.NombreLab;
                db.Entry(oLab).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Lab");
        }
        public async Task<IActionResult> EliminarLab(int IdLab)
        {
            EditarLabViewModel model = new EditarLabViewModel();
            using (var db = new DbReservasContext())
            {
                var oLab = db.TbLabs.Find(IdLab);
                _context.TbLabs.Remove(oLab);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lab");
            }
            return RedirectToAction("Index", "Lab");
        }
    }
}
