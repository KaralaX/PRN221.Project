using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRN221.Project.Domain.Entities;

namespace PRN221.WebUi.Areas.Doctors.Pages;

public class Index : PageModel
{
    private readonly Project.Infrastructure.Persistence.ApplicationDbContext _context;

    public Index(Project.Infrastructure.Persistence.ApplicationDbContext context)
    {
        _context = context;
    }

    public string CurrentFilter { get; set; }
    public IList<Doctor> Doctor { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Doctors != null)
        {
            Doctor = await _context.Doctors.ToListAsync();
        }
    }


    public async Task OnPostAsync(string searchString)
    {
        CurrentFilter = searchString;

        IQueryable<Doctor> doctorIQ = from s in _context.Doctors
                                     select s;

        if (!String.IsNullOrEmpty(searchString))
        {
            doctorIQ = doctorIQ.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
        }

        Doctor = doctorIQ.ToList();
    }

}