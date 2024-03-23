using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Persistence;
using PRN221.WebUi.Hubs;

namespace PRN221.WebUi.Areas.Departments.Pages.Manage;
[Authorize(Roles = Roles.Admin)]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<SignalRServer> _signalRHub;
    public EditModel(ApplicationDbContext context, IHubContext<SignalRServer> signalRHub)
    {
        _context = context;
        _signalRHub = signalRHub;
    }

    [BindProperty]
    public Department Department { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department =  await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);
        if (department == null)
        {
            return NotFound();
        }
        Department = department;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Department).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();

            await _signalRHub.Clients.All.SendAsync("LoadDepartments");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(Department.Id))
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

    private bool DepartmentExists(Guid id)
    {
        return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}