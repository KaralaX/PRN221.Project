using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.ServiceReviews.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceReview ServiceReview { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.ServiceReviews == null)
            {
                return NotFound();
            }

            var servicereview = await _context.ServiceReviews.FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (servicereview == null)
            {
                return NotFound();
            }
            ServiceReview = servicereview;
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Status");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ServiceReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceReviewExists(ServiceReview.AppointmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServiceReviewExists(Guid id)
        {
            return (_context.ServiceReviews?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
