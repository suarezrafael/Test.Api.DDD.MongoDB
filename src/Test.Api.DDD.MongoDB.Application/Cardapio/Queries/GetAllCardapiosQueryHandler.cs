using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Queries;

public class GetAllCardapiosQueryHandler : IRequestHandler<GetAllCardapiosQuery, IEnumerable<CardapioDto>>
{
    private readonly ICardapioRepository _repository;

    public GetAllCardapiosQueryHandler(ICardapioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CardapioDto>> Handle(GetAllCardapiosQuery request, CancellationToken cancellationToken)
    {
        var cardapios = await _repository.GetAllAsync();
        return cardapios.Select(c => new CardapioDto
        {
            Id = c.Id,
            Titulo = c.Titulo,
            Preco = c.Preco,
            Descricao = c.Descricao,
            PossuiPreparo = c.PossuiPreparo
        });
    }
}
