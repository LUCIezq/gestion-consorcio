using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.shared;

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

        public async Task<Consorcio?> BuscarConsorcioPorId(int id, Guid userId)
        {
            return await _dbContext.Consorcios.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        public async Task EliminarConsorcio(Consorcio consorcio)
        {
            _dbContext.Consorcios.Remove(consorcio);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Consorcio> GuardarConsorcio(Consorcio consorcio)
        {
            await _dbContext.Consorcios.AddAsync(consorcio);
            await _dbContext.SaveChangesAsync();
            return consorcio;
        }

        public async Task<ICollection<ConsorcioDetailViewModel>> ObtenerConsorcios(Guid userId)
        {
            return await _dbContext.Consorcios.Where(c => c.UserId == userId).Select(c => new ConsorcioDetailViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Calle = c.Calle,
                Ciudad = c.Ciudad,
                Provincia = c.Provincia,
                Latitud = c.Latitud.ToString(),
                Longitud = c.Longitud.ToString()
            }).ToListAsync();
        }

        public async Task<PaginatedList<ConsorcioDetailViewModel>> ObtenerConsorciosPaginados(Guid userId, int pageIndex, int pageSize)
        {
            var query = _dbContext.Consorcios
                .Where(c => c.UserId == userId)
                .Select(c => new ConsorcioDetailViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Calle = c.Calle,
                    Ciudad = c.Ciudad,
                    Provincia = c.Provincia,
                    Latitud = c.Latitud.ToString(),
                    Longitud = c.Longitud.ToString()
                });

            var count = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<ConsorcioDetailViewModel>(items, count, pageIndex, pageSize);
        }

        // public async Task<IEnumerable<ConsorcioCoordenadaViewModel>> ObtenerCoordenadas()
        // {
        //     return await _dbContext.Consorcios.Select(c => new ConsorcioCoordenadaViewModel
        //     {
        //         Id = c.Id,
        //         Nombre = c.Nombre,
        //         Latitud = c.Latitud.ToString(),
        //         Longitud = c.Longitud.ToString(),
        //         Calle = c.Calle,
        //         Ciudad = c.Ciudad
        //     }).ToListAsync();
        // }
    }
}
