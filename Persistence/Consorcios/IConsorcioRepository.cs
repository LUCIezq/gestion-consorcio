using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Consorcios.DTOs;

namespace PracticaParcial.Persistence.Consorcios
{
    public interface IConsorcioRepository
    {
        Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal);

        Task<Consorcio> GuardarConsorcio(Consorcio consorcio);

        Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerCoordenadas();

        Task<Consorcio?> BuscarConsorcioPorId(int id);

        Task EliminarConsorcio(Consorcio consorcio);

    }
}