using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Domain.Enums;
using PRN221.Project.Infrastructure.Persistence;
using PRN221.WebUi.Hubs;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<SignalRServer> _signalRHub;
    public DeleteModel(ApplicationDbContext context, IHubContext<SignalRServer> signalRHub)
    {
        _context = context;
        _signalRHub = signalRHub;
    }

    [BindProperty] public Appointment Appointment { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .Include(x => x.Service)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }

        Appointment = appointment;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(x => x.MedicalBill)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (appointment == null) return RedirectToPage("./Index");

        appointment.Status = AppointmentStatus.Cancelled.ToString();
        appointment.MedicalBill.Status = PaymentStatus.Cancelled.ToString();
        
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        await _signalRHub.Clients.All.SendAsync("LoadAppointments");
        return RedirectToPage("./Index");
    }
}