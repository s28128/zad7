using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication15.Models;
using WebApplication15.DTOs;
namespace WebApplication15.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly TripDbContext _context;

    public TripsController(TripDbContext context)
    {
        _context = context;
    }

    // GET: api/trips
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDTO>>> GetTrips()
    {
        var trips = await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new TripDTO
            {
                IdTrip = t.IdTrip,
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople
            })
            .ToListAsync();

        return trips;
    }
}