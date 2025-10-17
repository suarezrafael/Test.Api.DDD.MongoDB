using MediatR;
using Test.Api.DDD.MongoDB.Application.Usuario.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public record LoginCommand(LoginDto Login) : IRequest<string?>;
