using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.shared;

namespace PracticaParcial.Models.Consorcios
{
    public interface IConsorcioService
    {
        Task<GuardarConsorcioResponse> GuardarConsorcio(CreateConsorcioViewModel model, Guid userId);
        Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal);

        Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerCoordenadas(Guid userId);

        Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerConsorcios(Guid userId);

        Task<PaginatedList<ConsorcioDetailViewModel>> ObtenerConsorciosPaginados(Guid userId, int pageIndex, int pageSize);

        Task<EliminarConsorcioResponse> EliminarConsorcio(int id, Guid userId);

        Task<Consorcio?> ObtenerConsorcioPorId(int id, Guid userId);
    }
}
