using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;

namespace PRN221.WebUi.Areas.Doctors.Pages.Manage;

public class IndexModel : PageModel
{
    private readonly Project.Infrastructure.Persistence.ApplicationDbContext _context;

    public IndexModel(Project.Infrastructure.Persistence.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Doctor> Doctor { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Doctor = await _context.Doctors
            .ToListAsync();
    }
}