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
            Array lista;
            int lab = 1;
            string[,] semana = new String[13, 7];
            DateTime fecha = DateTime.Now, primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1)
            , ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);
            using (var context = _context)
            {
                var lst = from d in _context.TbReservas
                          where d.FechaReserva >= primerdia &&
                          d.FechaReserva <= ultimodia &&
                          d.IdLab == lab

                          select d;
                if (lst.Count()>0)
                {
                    TbReserva oReserva = lst.First();
                    lista = lst.ToArrayAsync().Wait();
                    for (int i = 1; i <= lst.Count(); i++)
                    {
                        

                    }
                }
            }

            var res = _context.TbReservas.Include(b => b.IdUsrNavigation)
                .Include(b => b.IdModuloNavigation)
                .Include(b => b.IdLabNavigation);

            return View(await res.ToListAsync());
        }
                
        }
        public IActionResult Create()
        {
            ViewData["Modulos"] = new SelectList(_context.TbReservas, "IdModulo", "InicioMod", "FinMod");

            return View();
        }
    }
}
