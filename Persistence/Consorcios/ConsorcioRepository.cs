using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Consorcios;

namespace PracticaParcial.Persistence.Consorcios
{
    public class ConsorcioRepository : IConsorcioRepository
    {
        private readonly UnidadDbContext _dbContext;

        public ConsorcioRepository(UnidadDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal)
        {
            return await _dbContext.Consorcios.FirstOrDefaultAsync(c =>
                c.Calle == calle &&
                c.Ciudad == ciudad &&
                c.Provincia == provincia &&
                c.CodigoPostal == codigoPostal);
        }

        public async Task<Consorcio> GuardarConsorcio(Consorcio consorcio)
        {
            await _dbContext.Consorcios.AddAsync(consorcio);
            await _dbContext.SaveChangesAsync();
            return consorcio;
        }
    }
}