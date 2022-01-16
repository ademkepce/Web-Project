using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje.Data;
using Proje.Models;

namespace Proje.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReservationAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            return await _context.Reservation.ToListAsync();
        }

        // GET: api/ReservationAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }
    }
}
