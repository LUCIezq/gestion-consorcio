using PracticaParcial.Persistence;
namespace PracticaParcial.Models.Unidades
{
    public interface IUnidadesLogica
    {
        void ActualizarUnidad(Unidad unidadExistente);
        void AgregarUnidad(Unidad unidad);
        void EliminarUnidad(int id);
        List<Unidad> ObtenerUnidades();
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
            return db.Unidades.ToList();
        }

        public Unidad? ObtenerUnidadPorId(int id)
        {
            return db.Unidades.FirstOrDefault(u => u.IdUnidad == id);
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
    }


}

