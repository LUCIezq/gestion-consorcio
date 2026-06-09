namespace PracticaParcial.Models.Gastos;

public interface IGastoService
{
    List<Gasto> ObtenerGastos();
    void Agregar(Gasto gasto);
    void Editar(Gasto gasto);
    void Eliminar(int id);
}
