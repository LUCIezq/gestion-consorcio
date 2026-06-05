using PracticaParcial.Models.Gastos.DTos;
using PracticaParcial.Persistence;


namespace PracticaParcial.Models.Gastos
{
    public interface IGastosLogica{
        void AgregarGasto(Gasto gasto);
        void EliminarGasto(int id);
        Gasto ObtenerGasto(int id);
        void ActualizarGasto(Gasto gasto);

        List<GastoViewModel> ObtenerGastosPorConsorcio(int idConsorcio);
        List<TipoGasto> ObtenerTiposGasto();

    }


    public class GastosLogica : IGastosLogica
    {
        private readonly UnidadDbContext _db;

        public GastosLogica(UnidadDbContext db)
        {
            this._db = db;
        }


        public void ActualizarGasto(Gasto gasto)
        {
            throw new NotImplementedException();
        }

        public void AgregarGasto(Gasto gasto)
        {
            _db.Gastos.Add(gasto);
            _db.SaveChanges();
        }

        public void EliminarGasto(int id)
        {
            throw new NotImplementedException();
        }

        public Gasto ObtenerGasto(int id)
        {
            throw new NotImplementedException();
        }

        public List<GastoViewModel> ObtenerGastosPorConsorcio(int idConsorcio)
        {
            var gastosEntidad= _db.Gastos.Where(g => g.IdConsorcio == idConsorcio).ToList();

            // 2. Mapeamos de forma segura fila por fila
            var gastosViewModel = gastosEntidad
                                   .Select(g => GastoViewModel.FromEntity(g))
                                   .ToList();

            return gastosViewModel;


        }

        public List<TipoGasto> ObtenerTiposGasto()
        {
            return _db.TiposGasto.ToList();
        }
    }
}
