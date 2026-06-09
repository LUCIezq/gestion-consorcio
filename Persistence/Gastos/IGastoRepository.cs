using PracticaParcial.Models.Gastos;

public interface IGastoRepository
{
    List<Gasto> ObtenerTodos();

    Gasto ObtenerPorId(int id);

    void Guardar(Gasto gasto);

    void Actualizar(Gasto gasto);

    void Eliminar(int id);
}