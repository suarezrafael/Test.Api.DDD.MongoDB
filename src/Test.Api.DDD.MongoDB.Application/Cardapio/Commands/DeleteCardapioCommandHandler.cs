using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;

namespace Test.Api.DDD.MongoDB.Application.Cardapio.Commands;

public class DeleteCardapioCommandHandler : IRequestHandler<DeleteCardapioCommand, bool>
{
    private readonly ICardapioRepository _repository;

    public DeleteCardapioCommandHandler(ICardapioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteCardapioCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
