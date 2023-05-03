using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Template.Core.Interfaces.Repositories;
using Template.Core.Models;
using Template.Core.ViewModels;

namespace Template.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<Models.AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly SignInManager<Models.AppUser> _signInManager;

    public AuthRepository(UserManager<Models.AppUser> userManager, IMapper mapper, SignInManager<Models.AppUser> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }
    public async Task<Response<AppUser>> Register(AppUser model)
    {
        var mainModel = _mapper.Map<Models.AppUser>(model);
        var result = await _userManager.CreateAsync(mainModel, model.Password!);
        if (result.Succeeded)
        {
            return new Response<AppUser>
            {
                Message = "Registration successful",
                Errors = null,
                Data = model,
                Success = true
            };
        }
        else
        {
            return new Response<AppUser>
            {
                Message = "Registration failed",
                Errors = result.Errors.Select(x => x.Description).ToList(),
                Data = null,
                Success = false
            };
        }
    }

    public async Task<Response<AppUser>> Login(LoginModel model)
    {
        Models.AppUser user;
        string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regexEmail = new Regex(patternEmail);
        bool isValidEmail = regexEmail.IsMatch(model.Email!);
        if (isValidEmail)
        {
            user = (await _userManager.FindByEmailAsync(model.Email!))!;
        }
        else
        {
            user = (await _userManager.FindByNameAsync(model.Email!))!;
        }

        if (user == null!)
        {
            return new Response<AppUser>
            {
                Message = "User not found",
                Errors = new List<string> { "User not found" },
                Data = null,
                Success = false
            };
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password!);
        if (result)
        {
            return new Response<AppUser>
            {
                Message = "Login successful",
                Errors = null,
                Data = null,
                Success = true
            };
        }
        else
        {
            return new Response<AppUser>
            {
                Message = "Login failed",
                Errors = new List<string> { "Invalid login details" },
                Data = null,
                Success = false
            };
        }
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}
