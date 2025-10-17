using MediatR;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public record UpdateUsuarioCommand(string Id, CreateUsuarioDto Usuario) : IRequest<UsuarioDto?>;
