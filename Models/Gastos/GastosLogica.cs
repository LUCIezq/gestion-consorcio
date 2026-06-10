

using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Gastos.DTos;
using PracticaParcial.Persistence;


namespace PracticaParcial.Models.Gastos
{
    public interface IGastosLogica
    {
        void AgregarGasto(Gasto gasto);
        void EliminarGasto(int id);
        GastoViewModel ObtenerGasto(int id);
        void ActualizarGasto(GastoViewModel gastoVM);

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



        public void ActualizarGasto(GastoViewModel gastoVM)
        {
            var gasto = _db.Gastos.FirstOrDefault(g => g.Id == gastoVM.Id);

            if (gasto == null) return;

            gasto.IdConsorcio = gastoVM.IdConsorcio;

            gasto.Nombre = gastoVM.Nombre;
            gasto.Monto = gastoVM.Monto;
            gasto.FechaGasto = gastoVM.FechaGasto;
            gasto.IdTipoGasto = gastoVM.IdTipoGasto;
            gasto.Descripcion = gastoVM.Descripcion;
            gasto.AnioExpensa = gastoVM.AnioExpensa;
            gasto.MesExpensa = gastoVM.MesExpensa;
            if (!string.IsNullOrEmpty(gastoVM.ArchivoComprobanteGuardado))
            {
                gasto.ArchivoComprobante = gastoVM.ArchivoComprobanteGuardado;
            }

            _db.SaveChanges();
        }

        public void AgregarGasto(Gasto gasto)
        {
            _db.Gastos.Add(gasto);
            _db.SaveChanges();
        }

        public void EliminarGasto(int id)
        {
            var gasto = this.ObtenerGastoPorId(id);
            _db.Gastos.Remove(gasto);
            _db.SaveChanges();
        }

        public GastoViewModel ObtenerGasto(int id)
        {
            var gasto = this.ObtenerGastoPorId(id);
            if (gasto == null)
            {
                return null;
            }


            var gastoViewModel = GastoViewModel.FromEntity(gasto);

            return gastoViewModel;
        }

        public List<GastoViewModel> ObtenerGastosPorConsorcio(int idConsorcio)
        {
            var gastosEntidad = _db.Gastos
                                   .Include(g => g.TipoGasto)
                                   .Where(g => g.IdConsorcio == idConsorcio)
                                   .OrderByDescending(g => g.FechaGasto)
                                   .ToList();


            var gastosViewModel = gastosEntidad
                                   .Select(g => GastoViewModel.FromEntity(g))
                                   .ToList();

            return gastosViewModel;


        }

        public List<TipoGasto> ObtenerTiposGasto()
        {
            return _db.TiposGasto.ToList();
        }
        

        private Gasto ObtenerGastoPorId(int id)
        {
            var gasto = _db.Gastos
                .Include(g => g.TipoGasto)
                .FirstOrDefault(g => g.Id == id);

            return gasto;
        }
    }
}
