using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Identity;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Staffs.Pages;

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
        return Page();
    }

    [BindProperty] public Staff Staff { get; set; } = default!;

    [BindProperty] public UserModel User { get; set; } = default!;

    public class UserModel
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Staff.UserId");
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

        await _userManager.AddToRoleAsync(user, Roles.Staff);

        Staff.UserId = user.Id;
        
        _context.Staffs.Add(Staff);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}