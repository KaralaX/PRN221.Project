using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;

namespace PRN221.WebUi.Areas.Departments.Pages;

public class DeleteModel : PageModel
{
    private readonly IApplicationDbContext _context;

    public DeleteModel(IApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Department Department { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);

        if (department == null)
        {
            return NotFound();
        }
        else 
        {
            Department = department;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var department = await _context.Departments.FindAsync(id);

        if (department != null)
        {
            Department = department;
            _context.Departments.Remove(Department);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}