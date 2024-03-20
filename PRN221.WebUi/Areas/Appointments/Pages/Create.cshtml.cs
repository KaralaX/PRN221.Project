using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.Project.Domain.Entities;

namespace PRN221.WebUi.Areas.Appointments.Pages;

public class CreateModel : PageModel
{
    private readonly Project.Infrastructure.Persistence.ApplicationDbContext _context;

    public CreateModel(Project.Infrastructure.Persistence.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "UserId");
        ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "UserId");
        ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Description");
        return Page();
    }

    [BindProperty]
    public Appointment Appointment { get; set; } = default!;
        
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.Appointments == null || Appointment == null)
        {
            return Page();
        }

        _context.Appointments.Add(Appointment);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}