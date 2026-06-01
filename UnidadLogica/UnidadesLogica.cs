using Consorcio.Entidades;

namespace Logica
{
    public interface IUnidadesLogica
    {
        void AgregarUnidad(Unidad unidad);
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
    }


}

