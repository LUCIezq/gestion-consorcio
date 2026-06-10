using Microsoft.EntityFrameworkCore;
using PracticaParcial.Persistence;
namespace PracticaParcial.Models.Unidades
{
    public interface IUnidadesLogica
    {
        void ActualizarUnidad(Unidad unidadExistente);
        void AgregarUnidad(Unidad unidad);
        void EliminarUnidad(int id);
        List<Unidad> ObtenerUnidades();
        List<Unidad> ObtenerUnidadesPorConsorcio(int idConsorcio);
        public Unidad? ObtenerUnidadPorId(int id);
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

        public Unidad ObtenerUnidadPorId(int id)
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

        public void EliminarUnidad(int id)
        {
            var unidad = ObtenerUnidadPorId(id);
            if (unidad != null)
            {
                db.Unidades.Remove(unidad);
                db.SaveChanges();
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

