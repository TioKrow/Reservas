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
        public async Task<IActionResult> Index(int dia, int mes, int año, int lab, int x)
        {
            DateTime fecha = new DateTime(año, mes, dia);
            fecha=fecha.AddDays(x);
            DateTime primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1);
            DateTime ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            ViewData["Lab"] = lab;
            ViewData["fecha"] = fecha;
            ViewData["dia"] = fecha.Day;
            ViewData["mes"] = fecha.Month;
            ViewData["año"] = fecha.Year;
            ViewData["x"] = 0;
            
            List<TbModulo> modulos = _context.TbModulos.ToList();
            ViewBag.modulos = modulos;

            ViewBag.Laboratorios = new SelectList(_context.TbLabs, "IdLab", "NombreLab");
            
            List<TbReserva> reservas = (from d in _context.TbReservas
                                             where d.FechaReserva >= primerdia &&
                                                       d.FechaReserva <= ultimodia &&
                                                       d.IdLab == lab
                                             select d).ToList();
            ViewBag.reservas = reservas;

            return View();
        }

        public IActionResult Create()
        {

            return View();
        }
    }
}
