using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Queries;

public class GetCardapioByIdQueryHandler : IRequestHandler<GetCardapioByIdQuery, CardapioDto?>
{
    private readonly ICardapioRepository _repository;

    public GetCardapioByIdQueryHandler(ICardapioRepository repository)
    {
        _repository = repository;
    }

    public async Task<CardapioDto?> Handle(GetCardapioByIdQuery request, CancellationToken cancellationToken)
    {
        var cardapio = await _repository.GetByIdAsync(request.Id);
        if (cardapio == null) return null;

        return new CardapioDto
        {
            Id = cardapio.Id,
            Titulo = cardapio.Titulo,
            Preco = cardapio.Preco,
            Descricao = cardapio.Descricao,
            PossuiPreparo = cardapio.PossuiPreparo
        };
    }
}
