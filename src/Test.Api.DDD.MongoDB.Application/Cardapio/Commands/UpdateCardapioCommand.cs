using MediatR;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Commands;

public record UpdateCardapioCommand(string Id, CreateCardapioDto Cardapio) : IRequest<CardapioDto?>;
