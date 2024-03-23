using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.ServiceReviews.Pages;

public class DoctorReviewModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DoctorReviewModel(PRN221.Project.Infrastructure.Persistence.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Appointment> Appointments { get; set; } = default!;
    public Doctor Doctor { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? Id)
    {
        var doctor = _context.Doctors.FirstOrDefault(x => x.Id.Equals(Id));

        if(doctor is null ) return NotFound();

        Doctor = doctor;

        Appointments = await _context.Appointments
                            .Include(x => x.ServiceReview)
                            .Include(a => a.Doctor)
                            .Include(a => a.Patient)
                            .Include(a => a.Service)
                            .Where(a => a.DoctorId.Equals(Id))
                            .ToListAsync();

        return Page();
    }
}
