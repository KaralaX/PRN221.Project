using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;

namespace PRN221.WebUi.Areas.Departments.Pages.Manage;

[Authorize(Roles = Roles.Admin)]
public class CreateModel : PageModel
{
    private readonly IApplicationDbContext _context;

    public CreateModel(IApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Department Department { get; set; } = default!;
        

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        _context.Departments.Add(Department);
        
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}