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
                };
                _context.Add(usr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roles"] = new SelectList(_context.TbRols, "IdRol", "NombreRol",model.RolUsr);

            return View();
        }
    }
}
