using MediatR; // Importante
using Lab10.Application2.UseCases.UserUseCase.Querys;
using Lab10.Domain2.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Lab10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        // Enviamos el comando a MediatR, que buscará de forma transparente el Handler respectivo
        var result = await _mediator.Send(new LoginCommand(loginRequest));

        if (!result.IsSuccess)
        {
            return Unauthorized(result); 
        }
        return Ok(result); 
    }
}