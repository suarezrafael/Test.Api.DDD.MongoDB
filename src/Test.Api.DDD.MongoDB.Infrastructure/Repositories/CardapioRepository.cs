using Microsoft.EntityFrameworkCore;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Domain.Entities;
using Test.Api.DDD.MongoDB.Infrastructure.Data;

namespace Test.Api.DDD.MongoDB.Infrastructure.Repositories;

public class CardapioRepository : ICardapioRepository
{
    private readonly ApplicationDbContext _context;

    public CardapioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cardapio?> GetByIdAsync(string id)
    {
        return await _context.Cardapios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cardapio>> GetAllAsync()
    {
        return await _context.Cardapios.ToListAsync();
    }

    public async Task<Cardapio> CreateAsync(Cardapio cardapio)
    {
        _context.Cardapios.Add(cardapio);
        await _context.SaveChangesAsync();
        return cardapio;
    }

    public async Task<Cardapio?> UpdateAsync(string id, Cardapio cardapio)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null) return null;

        existing.Titulo = cardapio.Titulo;
        existing.Preco = cardapio.Preco;
        existing.Descricao = cardapio.Descricao;
        existing.PossuiPreparo = cardapio.PossuiPreparo;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var cardapio = await GetByIdAsync(id);
        if (cardapio == null) return false;

        _context.Cardapios.Remove(cardapio);
        await _context.SaveChangesAsync();
        return true;
    }
}
