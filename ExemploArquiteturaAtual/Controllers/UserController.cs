using ExemploArquiteturaAtual.Business.Interfaces;
using ExemploArquiteturaAtual.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExemploArquiteturaAtual.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserBusiness _userBusiness;

    public UserController(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [Authorize]
    [HttpGet("recover-active-users")]
    public async Task<IActionResult> GetActiveUsers()
    {
        try
        {
            var data = await _userBusiness.RecoverActiveUsersAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {

            return Problem("An unexpected error ocurred! Try again later.", null, 500, "Unexpected Error!", null);
        }
    }

    [HttpPost("register-new-user")]
    public async Task<IActionResult> RegisterNewUser(RegisterUserDTO userRequest)
    {
        try
        {
            await _userBusiness.RegisterNewUserAsync(userRequest);

            return Created();
        }
        catch (Exception ex)
        {

            if (ex is InvalidDataException)
            {
                return BadRequest(ex.Message);
            }

            return Problem("An unexpected error ocurred! Try again later.", null, 500, "Unexpected Error!", null);
        }
    }
}

