﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;
using Reservas.Models.ViewModel;

using Microsoft.AspNetCore.Authorization;
namespace Reservas.Controllers
{
    [Authorize(Roles = "1") ]
    public class UsrController : Controller
    {
        private readonly DbReservasContext _context;

        public UsrController(DbReservasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int IdU)
        {
            ViewData["IdU"] = IdU;
            var usrs = _context.TbUsrs.Include(b => b.RolUsrNavigation);
            
            return View(await usrs.ToListAsync());
        }

        public IActionResult Create(int IdU)
        {
            ViewData["IdU"] = IdU;
            ViewData["Roles"] = new SelectList(_context.TbRols, "IdRol", "NombreRol");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsrViewModel model, int IdU)
        {
            ViewData["IdU"] = IdU;
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
            }
            ViewData["Roles"] = new SelectList(_context.TbRols, "IdRol", "NombreRol",model.RolUsr);

            return RedirectToAction("Index", "Usr", new { IdU = @IdU });
        }

        public IActionResult Modificar(int IdUsr, int IdU)
        {
            ViewData["IdU"] = IdU;
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
        public async Task<IActionResult> Modificar(EditarUsrViewModel model, int IdU)
        {
            ViewData["IdU"] = IdU;
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

            return RedirectToAction("Index","Usr", new {IdU=@IdU});
        }
        public async Task<IActionResult> Eliminar(int IdUsr, int IdU)
        {
            ViewData["IdU"] = IdU;
            EditarUsrViewModel model = new EditarUsrViewModel();
            using (var db = new DbReservasContext())
            {
                var oUser = db.TbUsrs.Find(IdUsr);
                _context.TbUsrs.Remove(oUser);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Usr", new {Idu=@IdU});
        }

    }
}
