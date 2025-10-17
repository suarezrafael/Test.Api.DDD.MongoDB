using MediatR;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public record DeleteUsuarioCommand(string Id) : IRequest<bool>;
