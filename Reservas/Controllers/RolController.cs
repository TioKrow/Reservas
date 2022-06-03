using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservas.Models;

using Microsoft.AspNetCore.Authorization;
namespace Reservas.Controllers
{
    [Authorize(Roles = "1")]
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
