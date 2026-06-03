using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Reserva;
using PracticaParcial.Models.Unidades;
using PracticaParcial.Models.Users;

namespace PracticaParcial.Persistence;

public class UnidadDbContext : DbContext
{
    public DbSet<Unidad> Unidades { get; set; }
    public DbSet<ReservaSUM> ReservasSUM { get; set; }

    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ConsorcioUnidadDb;Trusted_Connection=True;TrustServerCertificate=True");
    }
}
