using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.ServiceReviews.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ServiceReview> ServiceReview { get; set; } = default!;
        public IList<Appointment> Appointments { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Appointments != null)
            {
                Appointments = _context.Appointments
                                .Include(x => x.ServiceReview)
                                .Include(x=>x.Doctor)
                                .Include(x=>x.Patient)
                                .Include(x=>x.Service)
                                .ToList();
            }
        }
    }
}
