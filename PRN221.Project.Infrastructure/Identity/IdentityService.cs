using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PRN221.Project.Infrastructure.Identity;

public class IdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<IdentityUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<IdentityUser> userManager,
        IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<ErrorOr<string>> CreateUserAsync(string userName, string password)
    {
        var user = new IdentityUser()
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return user.Id;
        }

        var errors = result.Errors.Select(failure => Error.Failure(failure.Code, failure.Description));

        return ErrorOr<string>.From(errors.ToList());
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<ErrorOr<bool>> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : true;
    }

    private async Task<ErrorOr<bool>> DeleteUserAsync(IdentityUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.Succeeded
            ? true
            : ErrorOr<bool>.From(
                result.Errors.Select(
                    failure => Error.Failure(failure.Code, failure.Description)
                ).ToList()
            );
    }
}