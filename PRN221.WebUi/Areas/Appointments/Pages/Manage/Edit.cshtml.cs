using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Appointments.Pages.Manage;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Appointment Appointment { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FirstOrDefaultAsync(m => m.Id == id);
        if (appointment == null)
        {
            return NotFound();
        }

        Appointment = appointment;
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "UserId");
        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "UserId");
        ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Appointment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AppointmentExists(Appointment.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool AppointmentExists(Guid id)
    {
        return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}