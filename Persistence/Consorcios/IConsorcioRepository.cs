using PracticaParcial.Models.Consorcios;

namespace PracticaParcial.Persistence.Consorcios
{
    public interface IConsorcioRepository
    {
        Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal);

        Task<Consorcio> GuardarConsorcio(Consorcio consorcio);
    }
}