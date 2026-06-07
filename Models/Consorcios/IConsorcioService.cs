using PracticaParcial.Models.Consorcios.DTOs;

namespace PracticaParcial.Models.Consorcios
{
    public interface IConsorcioService
    {
        Task<GuardarConsorcioResponse> GuardarConsorcio(CreateConsorcioViewModel model);
        Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal);

        Task<IEnumerable<ConsorcioCoordenadaViewModel>> ObtenerCoordenadas();

        Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerConsorcios();

        Task<EliminarConsorcioResponse> EliminarConsorcio(int id);

        Task<Consorcio?> ObtenerConsorcioPorId(int id);
    }
}