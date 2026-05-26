namespace Lab10.Domain2.Dtos;

public class AuthResponseDto
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}