namespace Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

public class CreateUsuarioDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
