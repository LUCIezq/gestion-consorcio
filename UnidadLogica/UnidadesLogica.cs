using Consorcio.Entidades;

namespace Logica
{
    public interface IUnidadesLogica
    {
        void AgregarUnidad(Unidad unidad);
        void EliminarUnidad(int id);
        List<Unidad> ObtenerUnidades();
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

        public void AgregarUnidad(Unidad unidad)
        {
            db.Unidades.Add(unidad);
            db.SaveChanges();
        }

        public void EliminarUnidad(int id)
        {
            var unidad = ObtenerUnidades().FirstOrDefault(u => u.IdUnidad == id);
            if (unidad != null)
            {
                db.Unidades.Remove(unidad);
                db.SaveChanges();
            }
        }
    }


}

