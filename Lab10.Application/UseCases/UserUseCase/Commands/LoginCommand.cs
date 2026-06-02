using MediatR;
using Lab10.Application2.Interfaces;
using Lab10.Domain2.Dtos;
using Lab10.Domain2.Interfaces;

namespace Lab10.Application2.UseCases.UserUseCase.Querys;

// 1. El Comando (Request) con propiedades de tipo Record 
public record LoginCommand(LoginRequestDto LoginRequest) : IRequest<AuthResponseDto>;

// 2. El Handler Interno (Página 4 de tu guía)
internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.LoginRequest.Email);
        if (user == null)
        {
            return new AuthResponseDto { IsSuccess = false, Message = "Usuario no encontrado ❌" };
        }

        var isPasswordValid = _authService.VerifyPassword(user, user.PasswordHash, request.LoginRequest.Password);
        if (!isPasswordValid)
        {
            return new AuthResponseDto { IsSuccess = false, Message = "Contraseña incorrecta ❌" };
        }

        var token = _authService.GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Email = user.Email,
            Token = token,
            IsSuccess = true,
            Message = "¡Inicio de sesión exitoso! 🚀"
        };
    }
}