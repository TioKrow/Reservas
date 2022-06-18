using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;

namespace Reservas.Controllers
{
    public class ReservaController : Controller
    {
        private readonly DbReservasContext _context;

        public ReservaController(DbReservasContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            int lab = 1;
            DateTime fecha = DateTime.Now, primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1)
            , ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            var res = _context.TbReservas.Include(b => b.IdUsrNavigation)
                                         .Include(b => b.IdModuloNavigation)
                                         .Include(b => b.IdLabNavigation);
            using (var context = _context)
            {
                var lst = from d in res
                          where d.FechaReserva >= primerdia &&
                          d.FechaReserva <= ultimodia &&
                          d.IdLab == lab
                          select d;

                return View(await lst.ToListAsync());
            }

        }

        public IActionResult Create()
        {

            return View();
        }
    }
}
