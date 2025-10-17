using Microsoft.EntityFrameworkCore;
using Test.Api.DDD.MongoDB.Application.Interfaces;
using Test.Api.DDD.MongoDB.Domain.Entities;
using Test.Api.DDD.MongoDB.Infrastructure.Data;

namespace Test.Api.DDD.MongoDB.Infrastructure.Data;

public class DbSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public DbSeeder(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        var adminEmail = "admin@admin.com";
        var existingAdmin = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == adminEmail);

        if (existingAdmin == null)
        {
            var admin = new Usuario
            {
                Email = adminEmail,
                Senha = _passwordHasher.HashPassword("admin123")
            };

            _context.Usuarios.Add(admin);
            await _context.SaveChangesAsync();
        }
    }
}
