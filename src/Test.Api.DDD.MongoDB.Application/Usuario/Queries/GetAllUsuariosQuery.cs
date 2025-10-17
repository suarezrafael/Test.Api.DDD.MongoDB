using MediatR;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Queries;

public record GetAllUsuariosQuery : IRequest<IEnumerable<UsuarioDto>>;
