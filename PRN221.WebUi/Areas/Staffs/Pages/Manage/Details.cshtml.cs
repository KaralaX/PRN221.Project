using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Staffs.Pages.Manage;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Staff Staff { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var staff = await _context.Staffs.FirstOrDefaultAsync(m => m.Id == id);
        if (staff == null)
        {
            return NotFound();
        }
        else 
        {
            Staff = staff;
        }
        return Page();
    }
}