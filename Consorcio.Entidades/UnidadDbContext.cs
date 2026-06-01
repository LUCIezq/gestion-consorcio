using Microsoft.EntityFrameworkCore;

namespace Consorcio.Entidades
{
    public class UnidadDbContext :DbContext
    {

        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<ReservaSUM> ReservasSUM { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ConsorcioUnidadDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
