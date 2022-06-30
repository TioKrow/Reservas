using Microsoft.AspNetCore.Mvc;
using Reservas.Models;
using Reservas.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
            int IdU;
            using (var context = _context)
            {
                var lst = from d in _context.TbUsrs
                          where d.UsernameUsr == _usuario.UsernameUsr &&
                                d.PasswordUsr == _usuario.PasswordUsr 
                                
                          select d;
                if (lst.Count() > 0)
                {
                    TbUsr oUser = lst.First();
                    IdU = oUser.IdUsr;
                    var claims = new List<Claim> {
                     new Claim(ClaimTypes.Name,oUser.UsernameUsr),
                     new Claim(ClaimTypes.Role,Convert.ToString(oUser.RolUsr))
                    };

                    claims.Add(new Claim(ClaimTypes.Role, Convert.ToString(_usuario.RolUsr)));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home", new {IdU = @IdU});
                }
            }

            return View();
        }
        
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Acceso");
        }

    }
}
