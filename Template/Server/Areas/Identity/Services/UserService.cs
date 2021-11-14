using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Models;

namespace Template.Server.Areas.Identity.Services;

public interface IUserService
{
    public Task<Result> ConfirmEmail(User user, string token);
    public Task<Result> Create(User user, string password);
    public Task<User> GetCurrentUser(ClaimsPrincipal principal);
    public Task<User> GetUserByEmail(string email);
    public Task<User> GetUserById(string id);
    public Task<User> GetUserByUserName(string userName);
    public Task<string> GenerateEmailConfirmationToken(User user);
    public Task LogIn(User user);
    public Task<Result> LogIn(User user, string password);
    public Task LogOut();
    public Task<Result> SetPassword(User user, string currentPassword, string newPassword);
}

public class UserService : IUserService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public UserService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result> ConfirmEmail(User user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user, token);

        return new Result
        {
            HasSucceeded = result.Succeeded,
            Messages = result.Errors.Select(ie => ie.Description).ToArray()
        };
    }

    public async Task<Result> Create(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return new Result
        {
            HasSucceeded = result.Succeeded,
            Messages = result.Errors.Select(ie => ie.Description).ToArray()
        };
    }

    public async Task<string> GenerateEmailConfirmationToken(User user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<User> GetCurrentUser(ClaimsPrincipal principal)
    {
        return await _userManager.GetUserAsync(principal);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<User> GetUserByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task LogIn(User user)
    {
        await _signInManager.SignInAsync(user, isPersistent: true);
    }

    public async Task<Result> LogIn(User user, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);

        return new Result
        {
            HasSucceeded = result.Succeeded
        };
    }

    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<Result> SetPassword(User user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        return new Result
        {
            HasSucceeded = result.Succeeded,
            Messages = result.Errors.Select(ie => ie.Description).ToArray()
        };
    }
}
