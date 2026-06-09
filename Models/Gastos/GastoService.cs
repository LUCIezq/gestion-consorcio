namespace PracticaParcial.Models.Gastos;

public class GastoService : IGastoService
{
    private readonly IGastoRepository _gastoRepository;
    public GastoService(IGastoRepository gastoRepository)
    {
        this._gastoRepository = gastoRepository;
    }
    public void Agregar(Gasto gasto)
    {
        this._gastoRepository.Guardar(gasto);
    }

    public void Editar(Gasto gasto)
    {
        this._gastoRepository.Actualizar(gasto);
    }

    public void Eliminar(int id)
    {
        this._gastoRepository.Eliminar(id);
    }

    public List<Gasto> ObtenerGastos()
    {
        return this._gastoRepository.ObtenerTodos();
    }
}
