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

    public async Task OnGetAsync(string? searchString)
    {
        CurrentFilter = searchString ?? string.Empty;
        
        Doctor = await _context.Doctors
            .Where(x => searchString == null
                        || ((x.FirstName ?? string.Empty).Contains(searchString)
                            || (x.LastName ?? string.Empty).Contains(searchString)))
            .ToListAsync();
    }
}