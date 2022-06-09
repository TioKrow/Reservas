using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;
using Reservas.Models.ViewModel;

namespace Reservas.Controllers
{
    [Authorize(Roles = "1")]
    public class ModuloController : Controller
    {
        private readonly DbReservasContext _context;

        public ModuloController(DbReservasContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbModulos.ToListAsync());
        }
        public IActionResult AgregarMod()
        {
            ViewData["Modulos"] = new SelectList(_context.TbModulos, "IdModulo", "InicioMod", "FinMod");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarMod(ModuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mod = new TbModulo()
                {
                    InicioMod = model.InicioMod,
                    FinMod = model.FinMod,
                };
                _context.Add(mod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Modulos"] = new SelectList(_context.TbModulos, "IdModulo", "InicioMod", "FinMod");

            return View();
        }
        public async Task<IActionResult> ModificarMod(int IdModulo)
        {
            EditarModuloViewModel model = new EditarModuloViewModel();
            using (var db = new DbReservasContext())
            {
                var oMod = db.TbModulos.Find(IdModulo);
                model.InicioMod = oMod.InicioMod;
                model.FinMod = oMod.FinMod;
                model.IdModulo = oMod.IdModulo;
            }
            ViewData["Modulos"] = new SelectList(_context.TbModulos, "IdModulo", "InicioMod", "FinMod");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarMod(EditarModuloViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new DbReservasContext())
            {
                var oMod = db.TbModulos.Find(model.IdModulo);
                oMod.InicioMod = model.InicioMod;
                oMod.FinMod = model.FinMod;
                db.Entry(oMod).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Modulo");
        }
    }
}