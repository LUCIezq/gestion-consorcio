using Microsoft.EntityFrameworkCore;
using PracticaParcial.Persistence;
namespace PracticaParcial.Models.Unidades
{
    public interface IUnidadesLogica
    {
        void ActualizarUnidad(Unidad unidadExistente);
        void AgregarUnidad(Unidad unidad);
        Task EliminarUnidad(int id, Guid userId);
        List<Unidad> ObtenerUnidades();
        (List<Unidad> Unidades, int TotalRegistros) ObtenerUnidadesPorConsorcio(int idConsorcio, int pagina);
        public Unidad? ObtenerUnidadPorId(int id , Guid userId);
        Task<bool> ExisteUnidadEnConsorcio(string nombre, int idConsorcio, int? idUnidadAExcluir = null);
    }

    public class UnidadesLogica : IUnidadesLogica
    {
        private readonly UnidadDbContext db;

        public UnidadesLogica(UnidadDbContext db)
        {
            this.db = db;
        }

        public List<Unidad> ObtenerUnidades()
        {

            return db.Unidades
            .Include(u => u.Consorcio)
            .OrderBy(u => u.Nombre)
            .ToList();
        }

        public Unidad? ObtenerUnidadPorId(int id , Guid userId)
        {
            return db.Unidades
                     .Include(u => u.Consorcio)
                     .FirstOrDefault(u => u.IdUnidad == id);
        }

        public void AgregarUnidad(Unidad unidad)
        {
            db.Unidades.Add(unidad);
            db.SaveChanges();
        }

        public async Task EliminarUnidad(int id, Guid userId)
        {
            var unidad = ObtenerUnidadPorId(id, userId);
            if (unidad != null)
            {
                db.Unidades.Remove(unidad);
                await db.SaveChangesAsync();
            }
        }

        public void ActualizarUnidad(Unidad unidadExistente)
        {
            db.Unidades.Update(unidadExistente);
            db.SaveChanges();
        }

        public (List<Unidad> Unidades, int TotalRegistros) ObtenerUnidadesPorConsorcio(int idConsorcio, int pagina)
        {
            int cantidadPorPagina = 5;

            var query = db.Unidades
                .Include(u => u.Consorcio)
                .Where(u => u.Consorcio.Id == idConsorcio);

            int totalRegistros = query.Count();

            var unidades = query
                .OrderBy(u => u.Nombre)
                .Skip((pagina - 1) * cantidadPorPagina)
                .Take(cantidadPorPagina)
                .ToList();

            return (unidades, totalRegistros);
        }



        public async Task<bool> ExisteUnidadEnConsorcio(string nombre, int idConsorcio, int? idUnidadAExcluir = null)
        {
            var query = db.Unidades.Where(u => u.Nombre.ToLower() == nombre.ToLower() && u.Consorcio.Id == idConsorcio);

            if (idUnidadAExcluir.HasValue)
            {
                query = query.Where(u => u.IdUnidad != idUnidadAExcluir.Value);
            }

            return await query.AnyAsync();
        }


    }

}

