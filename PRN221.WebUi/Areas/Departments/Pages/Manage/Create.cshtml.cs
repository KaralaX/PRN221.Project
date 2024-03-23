using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;
using PRN221.WebUi.Hubs;

namespace PRN221.WebUi.Areas.Departments.Pages.Manage;

[Authorize(Roles = Roles.Admin)]
public class CreateModel : PageModel
{
    private readonly IApplicationDbContext _context;
    private readonly IHubContext<SignalRServer> _signalRHub;

    public CreateModel(IApplicationDbContext context, IHubContext<SignalRServer> signalRHub)
    {
        _context = context;
        _signalRHub = signalRHub;
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
        await _signalRHub.Clients.All.SendAsync("LoadDepartments");

        return RedirectToPage("./Index");
    }
}