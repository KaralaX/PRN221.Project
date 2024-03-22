using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Services.Pages.Manage;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var service = await _context.Services.FirstOrDefaultAsync(m => m.Id == id);

        if (service == null)
        {
            return NotFound();
        }

        Service = service;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var service = await _context.Services.FindAsync(id);

        if (service != null)
        {
            Service = service;
            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}