
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Template.Core.Interfaces.Services;
using Template.Core.Models;
using Template.Core.ViewModels;

namespace Template.Api.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Response<AppUser>>> Register(AppUser student)
    {
        if (student == null!)
        {
            return BadRequest(new Response<AppUser>
            {
                Message = "Model is null",
                Errors = new List<string> { "Model is null" },
                Data = null,
                Success = false
            });
        }

        var record = await _service.Register(student);
        if (!record.Success)
        {
            return BadRequest(new Response<AppUser>
            {
                Message = "Registration unsuccessful",
                Errors = record.Errors,
                Data = null,
                Success = false
            });
        }

        return Ok(new Response<AppUser>
        {
            Message = "Registration successful",
            Errors = null,
            Data = record.Data,
            Success = true
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<Response<AppUser>>> Login(LoginModel student)
    {
        if (student == null!)
        {
            return BadRequest(new Response<AppUser>
            {
                Message = "Model is null",
                Errors = new List<string> { "Model is null" },
                Data = null,
                Success = false
            });
        }

        var record = await _service.Login(student);
        if (!record.Success)
        {
            return BadRequest(new Response<AppUser>
            {
                Message = "Login unsuccessful",
                Errors = record.Errors,
                Data = null,
                Success = false
            });
        }

        return Ok(new Response<AppUser>
        {
            Message = "Login successful",
            Errors = null,
            Data = record.Data,
            Success = true
        });
    }
}
