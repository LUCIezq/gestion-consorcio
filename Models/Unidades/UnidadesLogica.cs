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
        (List<Unidad> Unidades, int TotalRegistros) ObtenerUnidadesPorConsorcio(int idConsorcio, int pagina);
        public Unidad? ObtenerUnidadPorId(int id);



        Task<bool> ExisteUnidadEnConsorcio(string nombre, int idConsorcio);
    }

    public class UnidadesLogica : IUnidadesLogica
    {
        private readonly UnidadDbContext db;

        public UnidadesLogica(UnidadDbContext db)
        {
            this.db = db;
        }


        public (List<Unidad> Unidades, int TotalRegistros) ObtenerUnidades(int idConsorcio, int pagina)
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



        public async Task<bool> ExisteUnidadEnConsorcio(string nombre, int idConsorcio)
        {
            return await db.Unidades
                .AnyAsync(u => u.Nombre.ToLower() == nombre.ToLower() && u.Consorcio.Id == idConsorcio);
        }


    }

}

