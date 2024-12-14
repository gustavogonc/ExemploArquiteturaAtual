using ExemploArquiteturaAtual.Business.Interfaces;
using ExemploArquiteturaAtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExemploArquiteturaAtual.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationBusiness _authenticationBusiness;
    public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
    {
        _authenticationBusiness = authenticationBusiness;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
    {
        try
        {
            var userData = await _authenticationBusiness.LoginAsync(loginRequest);

            if (userData is null)
            {
                return BadRequest("Invalid login data provided!");
            }

            return Ok(userData);

        }
        catch (Exception)
        {
            return Problem("An unexpected error ocurred! Try again later.", null, 500, "Unexpected Error!", null);
        }
    }
}
