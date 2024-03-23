using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Domain.Enums;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Departments.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get; set; } = default!;
        public IList<Service> Service { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Department = await _context.Departments.Include(x => x.Services.Where(x=>x.Status== true)).Where(x=>x.Status == true) .ToListAsync();

        }
    }
}
