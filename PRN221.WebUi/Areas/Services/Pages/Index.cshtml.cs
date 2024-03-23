using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Services.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Service> Service { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Service = await _context.Services
                .Include(s => s.Department).ToListAsync();
        }
    }
}
