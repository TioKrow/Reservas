using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;
using Reservas.Models.ViewModel;

namespace Reservas.Controllers
{
    public class UsrController : Controller
    {
        private readonly DbReservasContext _context;

        public UsrController(DbReservasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usrs = _context.TbUsrs.Include(b => b.RolUsrNavigation);

            return View(await usrs.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_context.TbRols, "IdRol", "NombreRol");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsrViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usr = new TbUsr()
                {
                    NombreUsr = model.NombreUsr,
                    ApellidoUsr = model.ApellidoUsr,
                    EmailUsr=model.EmailUsr,
                    FonoUsr=model.FonoUsr,
                    RolUsr=model.RolUsr,
                    UsernameUsr=model.UsernameUsr,
                    PasswordUsr=model.PasswordUsr,
                };
                _context.Add(usr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roles"] = new SelectList(_context.TbRols, "IdRol", "NombreRol",model.RolUsr);

            return View();
        }

        public async Task<IActionResult> Modificar(int IdUsr)
        {
            EditarUsrViewModel model = new EditarUsrViewModel();
            using (var db = new DbReservasContext())
            {
                var oUser = db.TbUsrs.Find(IdUsr);
                model.NombreUsr = oUser.NombreUsr;
                model.ApellidoUsr = oUser.ApellidoUsr;
                model.EmailUsr = oUser.EmailUsr;
                model.FonoUsr = oUser.FonoUsr;
                model.RolUsr = oUser.RolUsr;
                model.UsernameUsr = oUser.UsernameUsr;
                model.PasswordUsr = oUser.PasswordUsr;
                model.IdUsr = oUser.IdUsr;
            }
            ViewData["Roles"] = new SelectList(_context.TbRols, "IdRol", "NombreRol", model.RolUsr);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(EditarUsrViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new DbReservasContext())
            {
                var oUser = db.TbUsrs.Find(model.IdUsr);
                oUser.NombreUsr = model.NombreUsr;
                oUser.ApellidoUsr = model.ApellidoUsr;
                oUser.EmailUsr = model.EmailUsr;
                oUser.FonoUsr = model.FonoUsr;
                oUser.RolUsr = model.RolUsr;
                oUser.UsernameUsr = model.UsernameUsr;
                if (model.PasswordUsr != null && model.PasswordUsr.Trim() !="")
                {
                    oUser.PasswordUsr = model.PasswordUsr;
                }
                db.Entry(oUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
            }

            return RedirectToAction("Index","Usr");
        }
        public async Task<IActionResult> Eliminar(int IdUsr)
        {
            EditarUsrViewModel model = new EditarUsrViewModel();
            using (var db = new DbReservasContext())
            {
                var oUser = db.TbUsrs.Find(IdUsr);
                _context.TbUsrs.Remove(oUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Usr");
            }
            return RedirectToAction("Index", "Usr");
        }

    }
}
