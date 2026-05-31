using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consorcio.Entidades
{
    public class UnidadDbContext :DbContext
    {

        public DbSet<Unidad> Unidades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS02;Database=ConsorcioUnidadDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
