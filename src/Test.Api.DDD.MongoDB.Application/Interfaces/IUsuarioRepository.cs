namespace Test.Api.DDD.MongoDB.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Domain.Entities.Usuario?> GetByIdAsync(string id);
    Task<Domain.Entities.Usuario?> GetByEmailAsync(string email);
    Task<IEnumerable<Domain.Entities.Usuario>> GetAllAsync();
    Task<Domain.Entities.Usuario> CreateAsync(Domain.Entities.Usuario usuario);
    Task<Domain.Entities.Usuario?> UpdateAsync(string id, Domain.Entities.Usuario usuario);
    Task<bool> DeleteAsync(string id);
}
