using Microsoft.AspNetCore.Identity;
using Template.Shared;
using Template.Shared.Models;

namespace Template.Server.Services;

public interface IUserService
{
    public Task<Result> Create(User user, string password);
    public Task<User> GetUserByEmail(string email);
    public Task<User> GetUserByUserName(string userName);
    public Task LogIn(User user);
    public Task<Result> LogIn(User user, string password);
    public Task LogOut();
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

    public async Task<Result> Create(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return new Result
        {
            HasSucceeded = result.Succeeded,
            Messages = result.Errors.Select(ie => ie.Description).ToArray()
        };
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
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
}
