using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;

namespace Reservas.Controllers
{
    public class RolController : Controller
    {
        private readonly DbReservasContext _context;

        public RolController(DbReservasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TbRols.ToListAsync());
        }
    }
}
