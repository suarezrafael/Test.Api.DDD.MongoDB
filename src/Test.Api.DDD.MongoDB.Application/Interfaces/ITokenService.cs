namespace Test.Api.DDD.MongoDB.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Domain.Entities.Usuario usuario);
}
