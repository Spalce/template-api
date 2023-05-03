using Template.Core.Models;
using Template.Core.ViewModels;

namespace Template.Core.Interfaces.Services;

public interface IAuthService
{
    Task<Response<AppUser>> Register(AppUser model);
    Task<Response<AppUser>> Login(LoginModel model);
    Task Logout();
}
