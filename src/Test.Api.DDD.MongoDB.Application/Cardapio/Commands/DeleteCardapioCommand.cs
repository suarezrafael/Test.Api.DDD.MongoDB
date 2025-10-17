using MediatR;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Commands;

public record DeleteCardapioCommand(string Id) : IRequest<bool>;
