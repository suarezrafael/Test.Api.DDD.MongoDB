using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string?>
{
    private readonly IUsuarioRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUsuarioRepository repository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _repository.GetByEmailAsync(request.Login.Email);
        if (usuario == null) return null;

        if (!_passwordHasher.VerifyPassword(request.Login.Senha, usuario.Senha))
            return null;

        return _tokenService.GenerateToken(usuario);
    }
}
