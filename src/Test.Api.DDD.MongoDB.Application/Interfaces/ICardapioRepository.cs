namespace Test.Api.DDD.MongoDB.Application.Interfaces;

public interface ICardapioRepository
{
    Task<Domain.Entities.Cardapio?> GetByIdAsync(string id);
    Task<IEnumerable<Domain.Entities.Cardapio>> GetAllAsync();
    Task<Domain.Entities.Cardapio> CreateAsync(Domain.Entities.Cardapio cardapio);
    Task<Domain.Entities.Cardapio?> UpdateAsync(string id, Domain.Entities.Cardapio cardapio);
    Task<bool> DeleteAsync(string id);
}
