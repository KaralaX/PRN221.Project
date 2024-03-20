using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Services.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PRN221.Project.Infrastructure.Persistence.ApplicationDbContext _context;

        public IndexModel(PRN221.Project.Infrastructure.Persistence.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Service> Service { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Services != null)
            {
                Service = await _context.Services
                .Include(s => s.Department).ToListAsync();
            }
        }
    }
}
