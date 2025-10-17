using MediatR;
using Test.Api.DDD.MongoDB.Application.Interfaces;

namespace Test.Api.DDD.MongoDB.Application.Usuario.Commands;

public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, bool>
{
    private readonly IUsuarioRepository _repository;

    public DeleteUsuarioCommandHandler(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
