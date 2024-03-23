using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Doctors.Pages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Doctor Doctor { get; set; } = default!;
    public double Rating { get; set; } = default!;
    public IList<Appointment> Appointments { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors.FirstOrDefaultAsync(m => m.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        Doctor = doctor;
        
        var reviews = _context.ServiceReviews.Include(x=>x.Appointment);

        if (reviews.Any())
        {
            Rating = reviews.Average(x => x.Rating);
        }
        else
        {
            Rating = 0;
        }        
        Appointments = await _context.Appointments
            .Include(x => x.ServiceReview)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Service)
            .Where(a => a.DoctorId.Equals(id))
            .ToListAsync();

        return Page();
    }
}