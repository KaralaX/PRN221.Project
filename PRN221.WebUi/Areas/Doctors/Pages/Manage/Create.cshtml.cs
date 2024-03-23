using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Infrastructure.Identity;
using PRN221.Project.Infrastructure.Persistence;

namespace PRN221.WebUi.Areas.Doctors.Pages.Manage;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGet()
    {
        ViewData["Services"] = new SelectList(await _context.Services.ToListAsync(), "Id", "Name");
        
        return Page();
    }

    [BindProperty] public DoctorModel Doctor { get; set; } = default!;

    public class DoctorModel
    {
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public ICollection<Guid> Services { get; set; } = new HashSet<Guid>();
    }
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

        var services = _context.Services.Where(x => Doctor.Services.Contains(x.Id));
        
        var newDoctor = new Doctor
        {
            Address = Doctor.Address,
            FirstName = Doctor.FirstName,
            LastName = Doctor.LastName,
            Dob = Doctor.Dob,
            Gender = Doctor.Gender,
            UserId = user.Id,
            Services = services.ToList()
        };
        
        _context.Doctors.Add(newDoctor);
        
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}