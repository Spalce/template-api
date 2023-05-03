using Template.Core.Interfaces.Repositories;
using Template.Core.Interfaces.Services;
using Template.Core.Models;
using Template.Core.ViewModels;

namespace Template.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _service;

    public AuthService(IAuthRepository service)
    {
        _service = service;
    }

    public async Task<Response<AppUser>> Register(AppUser model)
    {
        try
        {
            return await _service.Register(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<AppUser>> Login(LoginModel model)
    {
        try
        {
            return await _service.Login(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task Logout()
    {
        try
        {
            return _service.Logout();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
