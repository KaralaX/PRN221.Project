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
    public class DoctorReviewModel : PageModel
    {
        private readonly PRN221.Project.Infrastructure.Persistence.ApplicationDbContext _context;

        public DoctorReviewModel(PRN221.Project.Infrastructure.Persistence.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Appointment> Appointments { get;set; } = default!;
        public Doctor Doctor { get; set; }

        public async Task OnGetAsync(Guid? doctorId)
        {
            if (_context.Appointments != null)
            {
                Appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.Service).Where(a => a.DoctorId.Equals(doctorId)).ToListAsync();

                Doctor = _context.Doctors.Where(x => x.Id.Equals(doctorId)).First();
            }
        }
    }
}
