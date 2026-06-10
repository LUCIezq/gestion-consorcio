using PracticaParcial.Models.Gastos;

public interface IGastoRepository
{
  Gasto ObtenerPorId(int id);
    List<Gasto> ObtenerGastosPorConsorcio(int idConsorcio);
    List<TipoGasto> ObtenerTiposGasto();
    void Agregar(Gasto gasto);
    void Eliminar(Gasto gasto);

    void Actualizar();
    
}