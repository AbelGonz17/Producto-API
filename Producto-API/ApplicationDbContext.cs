

using Microsoft.EntityFrameworkCore;
using Producto_API.Entidades;

namespace Producto_API;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasColumnType("decimal(18,2)");

    }

    public DbSet<Producto> Productos { get; set; }

}

