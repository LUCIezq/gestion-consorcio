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
        List<Unidad> ObtenerUnidadesPorConsorcio(int idConsorcio);
        public Unidad? ObtenerUnidadPorId(int id, Guid userId);
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

        public Unidad? ObtenerUnidadPorId(int id, Guid userId)
        {
            return db.Unidades.Include(u => u.Consorcio).FirstOrDefault(u => u.IdUnidad == id && u.Consorcio.UserId == userId);
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

        public List<Unidad> ObtenerUnidadesPorConsorcio(int idConsorcio)
        {
            return db.Unidades
                     .Include(u => u.Consorcio)
                     .Where(u => u.Consorcio.Id == idConsorcio)
                     .OrderBy(u => u.Nombre)
                     .ToList();
        }
    }


}

