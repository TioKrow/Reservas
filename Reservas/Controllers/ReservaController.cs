using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;
using Reservas.Models.ViewModel;
namespace Reservas.Controllers
{
    public class ReservaController : Controller
    {
        private readonly DbReservasContext _context;

        public ReservaController(DbReservasContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int dia, int mes, int año, int lab, int x, int xy, int IdU)
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
            ViewData["xy"] = xy;
            ViewData["IdU"] = IdU;

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

        public IActionResult Create(int dia, int mes, int año, int lab, int xy, int IdU)
        {
            DateTime fecha = new DateTime(año, mes, dia);
            DateTime primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1);
            DateTime ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            ViewData["Lab"] = lab;
            ViewData["xy"] = xy;
            ViewData["fecha"] = fecha;
            ViewData["dia"] = fecha.Day;
            ViewData["mes"] = fecha.Month;
            ViewData["año"] = fecha.Year;
            ViewData["IdU"] = IdU;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReservaViewModel model, int dia, int mes, int año, int lab, int xy, int IdU)
        {
            DateTime fecha = new DateTime(año, mes, dia);
            DateTime primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1);
            DateTime ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            ViewData["Lab"] = lab;
            ViewData["xy"] = xy;
            ViewData["fecha"] = fecha;
            ViewData["dia"] = fecha.Day;
            ViewData["mes"] = fecha.Month;
            ViewData["año"] = fecha.Year;
            ViewData["IdU"] = IdU;
            if (ModelState.IsValid)
            {
                if (model.FinReserva < model.FechaReserva)
                {
                    return RedirectToAction("Index", "Reserva", new { Idu = @IdU, dia = @dia, mes = @mes, año = @año, lab = @lab });
                }
                for (DateTime fechaf = model.FechaReserva; fechaf <= model.FinReserva; fechaf = fechaf.AddDays(7))
                {
                    var reserva = new TbReserva()
                    {

                        IdUsr = model.IdUsr,
                        FechaReserva = fechaf,
                        IdModulo = model.IdModulo,
                        IdLab = model.IdLab,
                        Curso = model.Curso,
                        Docente = model.Docente,
                        FinReserva = model.FinReserva,
                    };
                    _context.Add(reserva);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index","Reserva", new {Idu=@IdU, dia=@dia, mes=@mes, año=@año, lab=@lab});
        }
        public IActionResult Modificar(int IdReserva,int dia, int mes, int año, int lab, int xy, int IdU)
        {
            EditarReservaViewModel model = new EditarReservaViewModel();
            using (var db = new DbReservasContext())
            {
                var oReserva = db.TbReservas.Find(IdReserva);
                model.IdReserva = oReserva.IdReserva;
                model.IdLab = oReserva.IdLab;
                model.IdModulo = oReserva.IdModulo;
                model.FechaReserva = oReserva.FechaReserva;
                model.FinReserva = oReserva.FinReserva;
                model.Curso = oReserva.Curso;
                model.Docente = oReserva.Docente;
                model.IdUsr = oReserva.IdUsr;
            }

            DateTime fecha = new DateTime(año, mes, dia);
            DateTime primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1);
            DateTime ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            ViewData["Lab"] = lab;
            ViewData["xy"] = xy;
            ViewData["fecha"] = fecha;
            ViewData["dia"] = fecha.Day;
            ViewData["mes"] = fecha.Month;
            ViewData["año"] = fecha.Year;
            ViewData["IdU"] = IdU;
             
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(EditarReservaViewModel model,int dia, int mes, int año, int lab, int xy, int IdU)
        {
            DateTime fecha = new DateTime(año, mes, dia);
            DateTime primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1);
            DateTime ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            ViewData["Lab"] = lab;
            ViewData["fecha"] = fecha;
            ViewData["dia"] = fecha.Day;
            ViewData["mes"] = fecha.Month;
            ViewData["año"] = fecha.Year;
            ViewData["xy"] = xy;
            ViewData["IdU"] = IdU;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (IdU !=1 && IdU !=model.IdUsr)
            {
                return RedirectToAction("Index", "Reserva", new { Idu = @IdU, dia = @dia, mes = @mes, año = @año, lab = @lab });
            }
            if (model.FinReserva < model.FechaReserva)
            {
                return RedirectToAction("Index", "Reserva", new { Idu = @IdU, dia = @dia, mes = @mes, año = @año, lab = @lab });
            }

            using (var db = new DbReservasContext())
            {
                var oReserva = db.TbReservas.Find(model.IdReserva);
                for (DateTime fechaf = model.FechaReserva; fechaf <= model.FinReserva; fechaf = fechaf.AddDays(7))
                {
                oReserva.IdReserva = model.IdReserva;
                oReserva.IdLab = model.IdLab;
                oReserva.IdModulo = model.IdModulo;
                oReserva.FechaReserva = fechaf;
                oReserva.FinReserva = model.FinReserva;
                oReserva.Curso = model.Curso;
                oReserva.Docente = model.Docente;
                oReserva.IdUsr = model.IdUsr;
                db.Entry(oReserva).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

            }

            return RedirectToAction("Index", "Reserva", new { Idu = @IdU, dia = @dia, mes = @mes, año = @año, lab = @lab });
        }
        public async Task<IActionResult> Eliminar(int IdReserva, int dia, int mes, int año, int lab, int xy, int IdU)
        {
            DateTime fecha = new DateTime(año, mes, dia);
            DateTime primerdia = fecha.AddDays(-(byte)fecha.DayOfWeek + 1);
            DateTime ultimodia = fecha.AddDays(6 - (byte)fecha.DayOfWeek);

            ViewData["Lab"] = lab;
            ViewData["fecha"] = fecha;
            ViewData["dia"] = fecha.Day;
            ViewData["mes"] = fecha.Month;
            ViewData["año"] = fecha.Year;
            ViewData["xy"] = xy;
            ViewData["IdU"] = IdU;
            EditarReservaViewModel model = new EditarReservaViewModel();
            using (var db = new DbReservasContext())
            {
                var oReserva = db.TbReservas.Find(IdReserva);
                _context.TbReservas.Remove(oReserva);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Reserva", new { Idu = @IdU, dia = @dia, mes = @mes, año = @año, lab = @lab });
        }
    }
}
