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
        public async Task<IActionResult> Index(UsrViewModel _usuario)
        {
            using (var context = _context)
            {
                var lst = from d in _context.TbUsrs
                          where d.UsernameUsr == _usuario.UsernameUsr && 
                                d.PasswordUsr == _usuario.PasswordUsr
                          select d;
                if (lst.Count() > 0)
                {
                    var claims = new List<Claim> {
                     new Claim(ClaimTypes.Name,_usuario.UsernameUsr),
                     new Claim(ClaimTypes.Role,Convert.ToString(_usuario.RolUsr))
                    };

                    claims.Add(new Claim(ClaimTypes.Role, Convert.ToString(_usuario.RolUsr)));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));



                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        /*[HttpPost]
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
        }*/
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Acceso");
        }

    }
}
