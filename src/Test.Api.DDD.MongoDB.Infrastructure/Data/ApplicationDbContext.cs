using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Test.Api.DDD.MongoDB.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Usuario> Usuarios { get; set; }
    public DbSet<Domain.Entities.Cardapio> Cardapios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Domain.Entities.Usuario>().ToCollection("usuarios");
        modelBuilder.Entity<Domain.Entities.Cardapio>().ToCollection("cardapios");
    }
}
