using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Commands;

public class UpdateCardapioCommandHandler : IRequestHandler<UpdateCardapioCommand, CardapioDto?>
{
    private readonly ICardapioRepository _repository;

    public UpdateCardapioCommandHandler(ICardapioRepository repository)
    {
        _repository = repository;
    }

    public async Task<CardapioDto?> Handle(UpdateCardapioCommand request, CancellationToken cancellationToken)
    {
        var cardapio = new Domain.Entities.Cardapio
        {
            Titulo = request.Cardapio.Titulo,
            Preco = request.Cardapio.Preco,
            Descricao = request.Cardapio.Descricao,
            PossuiPreparo = request.Cardapio.PossuiPreparo
        };

        var updated = await _repository.UpdateAsync(request.Id, cardapio);
        if (updated == null) return null;

        return new CardapioDto
        {
            Id = updated.Id,
            Titulo = updated.Titulo,
            Preco = updated.Preco,
            Descricao = updated.Descricao,
            PossuiPreparo = updated.PossuiPreparo
        };
    }
}
