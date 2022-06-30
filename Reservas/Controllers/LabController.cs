using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;
using Reservas.Models.ViewModel;

using Microsoft.AspNetCore.Authorization;
namespace Reservas.Controllers
{
    [Authorize(Roles = "1")]
    public class LabController : Controller
    {
        private readonly DbReservasContext _context;

        public LabController(DbReservasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int IdU)
        {
            ViewData["IdU"]= IdU;
            return View(await _context.TbLabs.ToListAsync());
        }

        public IActionResult AgregarLab(int IdU)
        {
            ViewData["IdU"] = IdU;
            ViewData["Laboratorios"] = new SelectList(_context.TbLabs, "IdLab", "NombreLab", "DescripcionLab", "CapacidadLab");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarLab(LabViewModel model, int IdU)
        {
            ViewData["IdU"] = IdU;
            if (ModelState.IsValid)
            {
                var lab = new TbLab()
                {
                    NombreLab = model.NombreLab,
                    DescripcionLab = model.DescripcionLab,
                    CapacidadLab = model.CapacidadLab,

                };
                _context.Add(lab);
                await _context.SaveChangesAsync();
            }
            ViewData["Laboratorios"] = new SelectList(_context.TbLabs, "IdLab", "NombreLab");

            return RedirectToAction("Index", "Lab", new { IdU = @IdU });
        }
        public async Task<IActionResult> ModificarLab(int IdLab,int IdU)
        {
            ViewData["IdU"] = IdU;
            EditarLabViewModel model = new EditarLabViewModel();
            using (var db = new DbReservasContext())
            {
                var oLab = db.TbLabs.Find(IdLab);
                model.NombreLab = oLab.NombreLab;
                model.IdLab = oLab.IdLab;
                model.DescripcionLab = oLab.DescripcionLab;
                model.CapacidadLab = oLab.CapacidadLab;
            }
            ViewData["Laboratorios"] = new SelectList(_context.TbLabs, "IdLab", "NombreLab");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModificarLab(EditarLabViewModel model, int IdU)
        {
            ViewData["IdU"] = IdU;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new DbReservasContext())
            {
                var oLab = db.TbLabs.Find(model.IdLab);
                oLab.NombreLab = model.NombreLab;
                oLab.DescripcionLab = model.DescripcionLab;
                oLab.CapacidadLab= model.CapacidadLab;
                db.Entry(oLab).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Lab", new {IdU= @IdU});
        }
        public async Task<IActionResult> EliminarLab(int IdLab, int IdU)
        {
            ViewData["IdU"] = IdU;
            EditarLabViewModel model = new EditarLabViewModel();
            using (var db = new DbReservasContext())
            {
                var oLab = db.TbLabs.Find(IdLab);
                db.TbLabs.Remove(oLab);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Lab", new {IdU = @IdU});
        }
    }
}
