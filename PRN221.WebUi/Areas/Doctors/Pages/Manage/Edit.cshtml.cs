using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Doctors.Pages.Manage;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public DoctorModel Doctor { get; set; } = default!;

    public class DoctorModel
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public ICollection<Guid> Services { get; set; } = new HashSet<Guid>();
    }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var doctor = await _context.Doctors.Include(x => x.Services).FirstOrDefaultAsync(m => m.Id == id);
        if (doctor == null)
        {
            return NotFound();
        }

        Doctor = new DoctorModel
        {
            Id = doctor.Id,
            Address = doctor.Address,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Dob = doctor.Dob,
            Gender = doctor.Gender,
            Services = doctor.Services.Select(x => x.Id).ToList()
        };
        
        ViewData["Services"] = new SelectList(await _context.Services.ToListAsync(), "Id", "Name");
        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var doctor = await _context.Doctors
            .Include(x => x.Services)
            .FirstOrDefaultAsync(x => x.Id == Doctor.Id);

        if (doctor is null) return NotFound();
        
        doctor.Address = Doctor.Address;
        doctor.FirstName = Doctor.FirstName;
        doctor.LastName = Doctor.LastName;
        doctor.Dob = Doctor.Dob;
        doctor.Gender = Doctor.Gender;
        doctor.Services.Clear();
        
        var services = _context.Services.Where(x => Doctor.Services.Contains(x.Id));
        doctor.Services.AddRange(services);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DoctorExists(Doctor.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private bool DoctorExists(Guid id)
    {
        return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}