using AnimeNotes.Controllers.Requests;
using AnimeNotes.Db.Context;
using AnimeNotes.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeNotes.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(
    DataContext context,
    IAuthService authService): ControllerBase
{
    [HttpGet("api/me")]
    public async Task<IActionResult> GetMe(
        [FromHeader(Name ="Session")]string token,
        CancellationToken cancellationToken)
    {
        var session = await context.Sessions
            .FirstOrDefaultAsync(s => s.Token == token, cancellationToken);
        if (session is null)
            return Unauthorized();

        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == session.UserId, cancellationToken);


        return Ok(new {user!.Id});
    }

    [HttpPost("api/register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterDto registerDto,
        CancellationToken cancellationToken = default)
    {
        var result = await authService.Register(
            registerDto.Login,
            registerDto.Password, 
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok();
    }

    [HttpPost("api/login")]
    public async Task<IActionResult> Login(
        [FromBody] RegisterDto registerDto,
        CancellationToken cancellationToken = default)
    {
        var result = await authService.Login(
            registerDto.Login,
            registerDto.Password,
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new
        {
            session = result.Data
        });
    }
}
