using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, UsuarioDto?>
{
    private readonly IUsuarioRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUsuarioCommandHandler(IUsuarioRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto?> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = new Domain.Entities.Usuario
        {
            Email = request.Usuario.Email,
            Senha = _passwordHasher.HashPassword(request.Usuario.Senha)
        };

        var updated = await _repository.UpdateAsync(request.Id, usuario);
        if (updated == null) return null;

        return new UsuarioDto
        {
            Id = updated.Id,
            Email = updated.Email
        };
    }
}
