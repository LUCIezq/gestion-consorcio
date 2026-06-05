using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Gastos;
using PracticaParcial.Models.Reserva;
using PracticaParcial.Models.Unidades;
using PracticaParcial.Models.Users;
namespace PracticaParcial.Persistence;


public class UnidadDbContext : DbContext
{
    public DbSet<Unidad> Unidades { get; set; }
    public DbSet<ReservaSUM> ReservasSUM { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Consorcio> Consorcios { get; set; }

    public DbSet<Gasto> Gastos { get; set; }

    public DbSet<TipoGasto> TiposGasto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ConsorcioUnidadDb;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TipoGasto>().HasData(
            new TipoGasto { Id = 1, Nombre = "Mantenimiento Gral" },
            new TipoGasto { Id = 2, Nombre = "Reparacion Unidad" },
            new TipoGasto { Id = 3, Nombre = "Comprar Limpieza" },
            new TipoGasto { Id = 4, Nombre = "Extraordinario" }
        );
    }

}
