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
        public async Task<IActionResult> Index(int dia, int mes, int año, int lab, string fec)
        {
            //int lab = 1;
            //DateTime fecha = DateTime.Now
            //DateTime fecha = DateTime.Parse(fechaPaso);
            DateTime fec2 = Convert.ToDateTime(fec);
            //DateTime fecha = new DateTime(año, mes, dia);
            DateTime primerdia = fec2.AddDays(-(byte)fec2.DayOfWeek + 1);
            DateTime ultimodia = fec2.AddDays(6 - (byte)fec2.DayOfWeek);

            ViewData["Lab"] = lab;
            //ViewData["Fecha"] = fecha;
            ViewData["fec2"] = fec2;
            ViewData["dia"] = dia;
            ViewData["mes"] = mes;
            ViewData["año"] = año;
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
