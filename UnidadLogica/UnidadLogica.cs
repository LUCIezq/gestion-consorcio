using Consorcio.Entidades;

namespace Logica
{
    public interface IUnidadLogica
    {
        void AgregarUnidad(Unidad unidad);
        List<Unidad> ObtenerUnidades();
    }

    public class UnidadLogica : IUnidadLogica
    {
        private readonly UnidadDbContext db;

        public UnidadLogica(UnidadDbContext db)
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

