using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Services.Pages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Description");
        return Page();
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Service.Department");
        
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Services.Add(Service);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}