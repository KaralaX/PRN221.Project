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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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
            else
            {
                ServiceReview = servicereview;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.ServiceReviews == null)
            {
                return NotFound();
            }
            var servicereview = await _context.ServiceReviews.FindAsync(id);

            if (servicereview != null)
            {
                ServiceReview = servicereview;
                _context.ServiceReviews.Remove(ServiceReview);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
