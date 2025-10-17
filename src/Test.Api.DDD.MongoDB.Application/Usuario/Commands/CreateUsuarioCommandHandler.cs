using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, UsuarioDto>
{
    private readonly IUsuarioRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUsuarioCommandHandler(IUsuarioRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = new Domain.Entities.Usuario
        {
            Email = request.Usuario.Email,
            Senha = _passwordHasher.HashPassword(request.Usuario.Senha)
        };

        var created = await _repository.CreateAsync(usuario);

        return new UsuarioDto
        {
            Id = created.Id,
            Email = created.Email
        };
    }
}
