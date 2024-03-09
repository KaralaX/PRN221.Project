using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Infrastructure.Identity;

namespace PRN221.Project.Infrastructure.Persistence;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContext> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IOptions<AdminSettings> _adminOptions;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContext> logger,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context,
        IOptions<AdminSettings> adminOptions)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _adminOptions = adminOptions;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initializing the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (await _roleManager.RoleExistsAsync(Roles.Admin)) return;
        
        await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        await _roleManager.CreateAsync(new IdentityRole(Roles.Patient));
        await _roleManager.CreateAsync(new IdentityRole(Roles.Doctor));

        await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = _adminOptions.Value.UserName,
            Email = _adminOptions.Value.Email,
        }, password: _adminOptions.Value.Password);

        var adminUser = _context.Users.FirstOrDefault(x => x.Email == _adminOptions.Value.Email);

        if (adminUser != null)
        {
            await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }
}

public class AdminSettings
{
    public const string SectionName = "AdminSettings";
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}