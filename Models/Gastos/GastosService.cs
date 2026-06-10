

using Microsoft.EntityFrameworkCore;
using PracticaParcial.Models.Gastos.DTos;
using PracticaParcial.Persistence;


namespace PracticaParcial.Models.Gastos
{
    public interface IGastosService
    {
        void AgregarGasto(Gasto gasto);
        void EliminarGasto(int id);
        GastoViewModel ObtenerGasto(int id);
        void ActualizarGasto(GastoViewModel gastoVM);

        List<GastoViewModel> ObtenerGastosPorConsorcio(int idConsorcio);
        List<TipoGasto> ObtenerTiposGasto();

    }


    public class GastosService : IGastosService
    {
    

        private readonly IGastoRepository _gastoRepository;

        public GastosService(IGastoRepository gastoRepository)
        {
            _gastoRepository = gastoRepository;
        }

        public void ActualizarGasto(GastoViewModel gastoVM)
        {
            var gasto =this.ObtenerGastoPorId(gastoVM.Id);

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

           _gastoRepository.Actualizar();
        }

        public void AgregarGasto(Gasto gasto)
        {
           _gastoRepository.Agregar(gasto);
        }

        public void EliminarGasto(int id)
        {
            var gasto = this.ObtenerGastoPorId(id);
            _gastoRepository.Eliminar(gasto);
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

            var gastos = _gastoRepository.ObtenerGastosPorConsorcio(idConsorcio);
            return gastos.Select(GastoViewModel.FromEntity).ToList();

        }

        public List<TipoGasto> ObtenerTiposGasto()
        {
            return _gastoRepository.ObtenerTiposGasto();
        }
        

        private Gasto ObtenerGastoPorId(int id)
        {
            return _gastoRepository.ObtenerPorId(id);
        }
    }
}
