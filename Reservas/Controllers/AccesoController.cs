using Microsoft.AspNetCore.Mvc;
using Reservas.Models;

using Reservas.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Reservas.Controllers
{
    public class AccesoController : Controller
    {
        private readonly DbReservasContext _context;

        public AccesoController(DbReservasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string UsernameUsr, string PasswordUsr)
        {
            using (var context = _context)
            {
                var lst = from d in _context.TbUsrs
                          where d.UsernameUsr == UsernameUsr && d.PasswordUsr == PasswordUsr
                          select d;
                if (lst.Count() > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
        }
        public IActionResult Salir()
        {
            return RedirectToAction("Index", "Acceso");
        }

    }
}
