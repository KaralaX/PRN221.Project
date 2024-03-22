using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Identity;

namespace PRN221.WebUi.Areas.Doctors.Pages.Manage;

public class CreateModel : PageModel
{
    private readonly Project.Infrastructure.Persistence.ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public CreateModel(Project.Infrastructure.Persistence.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGet()
    {
        ViewData["Services"] = new SelectList(await _context.Services.ToListAsync(), "Id", "Name");
        
        return Page();
    }

    [BindProperty] public Doctor Doctor { get; set; } = default!;
    [BindProperty] public UserModel User { get; set; } = default!;

    public class UserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Doctor.UserId");
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = User.Email,
            Email = User.Email
        }, User.Password);
        
        var user = await _userManager.FindByEmailAsync(User.Email);

        await _userManager.AddToRoleAsync(user, Roles.Doctor);

        Doctor.UserId = user.Id;
        
        _context.Doctors.Add(Doctor);
        
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}