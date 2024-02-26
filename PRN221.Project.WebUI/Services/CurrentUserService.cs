using System.Security.Claims;
using PRN221.Project.Application.Common.Services;

namespace PRN221.Project.WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string? UserId => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}