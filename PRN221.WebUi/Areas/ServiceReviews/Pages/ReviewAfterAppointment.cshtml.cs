using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.ServiceReviews.Pages
{
    public class ReviewAfterAppointmentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReviewAfterAppointmentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(Guid? appointmentId)
        {
            AppointmentId = appointmentId;
            return Page();
        }

        [BindProperty]
        public ServiceReview ServiceReview { get; set; } = default!;
        public Guid? AppointmentId { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ServiceReviews == null || ServiceReview == null)
            {
                return Page();
            }

            _context.ServiceReviews.Add(ServiceReview);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
