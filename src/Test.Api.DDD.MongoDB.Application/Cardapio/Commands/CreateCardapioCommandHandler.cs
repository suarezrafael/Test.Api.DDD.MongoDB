using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Application.Cardapio.DTOs;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Commands;

public class CreateCardapioCommandHandler : IRequestHandler<CreateCardapioCommand, CardapioDto>
{
    private readonly ICardapioRepository _repository;

    public CreateCardapioCommandHandler(ICardapioRepository repository)
    {
        _repository = repository;
    }

    public async Task<CardapioDto> Handle(CreateCardapioCommand request, CancellationToken cancellationToken)
    {
        var cardapio = new Domain.Entities.Cardapio
        {
            Titulo = request.Cardapio.Titulo,
            Preco = request.Cardapio.Preco,
            Descricao = request.Cardapio.Descricao,
            PossuiPreparo = request.Cardapio.PossuiPreparo
        };

        var created = await _repository.CreateAsync(cardapio);

        return new CardapioDto
        {
            Id = created.Id,
            Titulo = created.Titulo,
            Preco = created.Preco,
            Descricao = created.Descricao,
            PossuiPreparo = created.PossuiPreparo
        };
    }
}
