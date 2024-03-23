using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Identity;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.ServiceReviews.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Where(x=>x.PatientId.Equals(userId)), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ServiceReview ServiceReview { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ServiceReviews == null || ServiceReview == null)
            {
                ViewData["AppointmentId"] = new SelectList(_context.Appointments, "Id", "Id");
                return Page();
            }

            _context.ServiceReviews.Add(ServiceReview);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
