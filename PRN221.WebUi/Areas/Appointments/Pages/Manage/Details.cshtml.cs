using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Appointments.Pages.Manage;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Appointment Appointment { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Include(x => x.Service)
            .Include(x => x.MedicalBill)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (appointment == null)
        {
            return NotFound();
        }

        Appointment = appointment;

        return Page();
    }
}