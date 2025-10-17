using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Queries;

public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto?>
{
    private readonly IUsuarioRepository _repository;

    public GetUsuarioByIdQueryHandler(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsuarioDto?> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _repository.GetByIdAsync(request.Id);
        if (usuario == null) return null;

        return new UsuarioDto
        {
            Id = usuario.Id,
            Email = usuario.Email
        };
    }
}
