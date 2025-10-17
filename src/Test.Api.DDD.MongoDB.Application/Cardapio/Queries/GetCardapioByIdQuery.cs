using MediatR;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Queries;

public record GetCardapioByIdQuery(string Id) : IRequest<CardapioDto?>;
