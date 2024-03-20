using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;
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
        _context.Departments.AddRange(new List<Department>
        {
            new()
            {
                Name = "Neurology",
                Description = "Neurology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Neuromuscular disorders",
                        Description = "Neuromuscular disorders",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Child neurology",
                        Description = "Child neurology",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "General neurology",
                        Description = "General neurology",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Teleneurology",
                        Description = "Teleneurology",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Neurocritical care",
                        Description = "Neurocritical care",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Urology",
                Description = "Urology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Pediatric urology",
                        Description = "Pediatric urology",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Renal transplant",
                        Description = "Renal transplant",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Male infertility",
                        Description = "Male infertility",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Urologic oncology",
                        Description = "Urologic oncology",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Surgery",
                Description = "Surgery",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Gynecological surgery",
                        Description = "Gynecological surgery",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "General surgery",
                        Description = "General surgery",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Endocrine surgery",
                        Description = "Endocrine surgery",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Colon and rectal surgery",
                        Description = "Colon and rectal surgery",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Breast surgery",
                        Description = "Breast surgery",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Bariatric surgery",
                        Description = "Bariatric surgery",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Proctology",
                Description = "Proctology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Treatment of paraproctitis",
                        Description = "Treatment of paraproctitis",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Radio-wave treatment",
                        Description = "Radio-wave treatment",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Rectal cancer diagnosis",
                        Description = "Rectal cancer diagnosis",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Hemorrhoid treatment",
                        Description = "Hemorrhoid treatment",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Laser treatment of hemorrhoid",
                        Description = "Laser treatment of hemorrhoid",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Traumatology",
                Description = "Traumatology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Treatment of hallux valgus",
                        Description = "Treatment of hallux valgus",
                        Status = true
                    },
                    new()
                    {
                        Name = "Treatment of arthrosis",
                        Description = "Treatment of arthrosis",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Treatment of fractures",
                        Description = "Treatment of fractures",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Oncology",
                Description = "Oncology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Head & neck cancer",
                        Description = "Head & neck cancer",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Breast cancer",
                        Description = "Breast cancer",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Colon cancer",
                        Description = "Colon cancer",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Blood cancer",
                        Description = "Blood cancer",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Brain & spine cancer",
                        Description = "Brain & spine cancer",
                        Status = true,
                        Duration = 15
                    }
                }
            },

            new()
            {
                Name = "Dermatology",
                Description = "Dermatology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Pediatric dermatology",
                        Description = "Pediatric dermatology",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Cosmetic services",
                        Description = "Cosmetic services",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "General dermatology",
                        Description = "General dermatology",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Dermatoscopy",
                        Description = "Dermatoscopy",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Specialized treatments for skin",
                        Description = "Specialized treatments for skin",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Routine skin exams",
                        Description = "Routine skin exams",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Gynecology",
                Description = "Gynecology",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Testing for infection",
                        Description = "Testing for infection",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Cancer screenings",
                        Description = "Cancer screenings",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Family planning",
                        Description = "Family planning",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Menopause counseling",
                        Description = "Menopause counseling",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Fertility evaluation",
                        Description = "Fertility evaluation",
                        Status = true,
                        Duration = 15
                    }
                }
            },
            new()
            {
                Name = "Orthopedics",
                Description = "Orthopedics",
                Status = true,
                Services = new List<Service>
                {
                    new()
                    {
                        Name = "Trauma/fracture care",
                        Description = "Trauma/fracture care",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Joint replacement for hip",
                        Description = "Joint replacement for hip",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Foot/ankle surgery",
                        Description = "Foot/ankle surgery",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Sports medicine",
                        Description = "Sports medicine",
                        Status = true,
                        Duration = 15
                    },
                    new()
                    {
                        Name = "Hand/wrist surgery",
                        Description = "Hand/wrist surgery",
                        Status = true,
                        Duration = 15
                    }
                }
            }
        });


        if (await _roleManager.RoleExistsAsync(Roles.Admin)) return;
        await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        await _roleManager.CreateAsync(new IdentityRole(Roles.Patient));
        await _roleManager.CreateAsync(new IdentityRole(Roles.Doctor));
        await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = _adminOptions.Value.UserName,
            Email = _adminOptions.Value.Email
        }, password: _adminOptions.Value.Password);

        var adminUser = await _userManager.FindByEmailAsync(_adminOptions.Value.Email);

        if (adminUser != null)
        {
            await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }
}

public class AdminSettings
{
    public const string SectionName = "AdminSettings";
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}