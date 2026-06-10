using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.shared;

namespace PracticaParcial.Models.Consorcios
{
    public interface IConsorcioService
    {
        Task<GuardarConsorcioResponse> GuardarConsorcio(CreateConsorcioViewModel model, Guid userId);
        Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal);

        Task<IEnumerable<ConsorcioCoordenadaViewModel>> ObtenerCoordenadas();

        Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerConsorcios(Guid userId);

        Task<PaginatedList<ConsorcioDetailViewModel>> ObtenerConsorciosPaginados(Guid userId, int pageIndex, int pageSize);

        Task<EliminarConsorcioResponse> EliminarConsorcio(int id);

        Task<Consorcio?> ObtenerConsorcioPorId(int id);
    }
}
