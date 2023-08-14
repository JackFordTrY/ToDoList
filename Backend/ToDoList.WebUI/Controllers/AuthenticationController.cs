using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Contracts.User;
using ToDoList.Infrastructure.Interfaces.Services;

namespace ToDoList.WebUI.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<UserAuthRequest> _validator;

    public AuthenticationController(IUserService service, IValidator<UserAuthRequest> validator)
    {
        _userService = service;
        _validator = validator;
    }

    [HttpGet]
    [Route("current")]
    public Dictionary<string, string> LoadCurrentUser() =>
        User.Claims.ToDictionary(c => c.Type, c => c.Value);

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserAuthRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByCredentialsAsync(request, cancellationToken);

        var principal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("user_id", user.Id.ToString()),
                    new Claim("username", user.Username)
                },
                CookieAuthenticationDefaults.AuthenticationScheme));

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            properties: new AuthenticationProperties() { IsPersistent = true },
            principal: principal);

        return Ok();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(UserAuthRequest request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        await _userService.AddAsync(request, cancellationToken);

        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok();
    }
}
