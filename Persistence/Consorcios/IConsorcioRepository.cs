using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.shared;

namespace PracticaParcial.Persistence.Consorcios
{
    public interface IConsorcioRepository
    {
        Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal, Guid userId);

        Task EditarConsorcio(Consorcio consorcio);
        Task<Consorcio> GuardarConsorcio(Consorcio consorcio);

        // Task<IEnumerable<ConsorcioCoordenadaViewModel>> ObtenerCoordenadas();

        Task<Consorcio?> BuscarConsorcioPorId(int id, Guid userId);

        Task EliminarConsorcio(Consorcio consorcio);

        Task<ICollection<ConsorcioDetailViewModel>> ObtenerConsorcios(Guid userId);

        Task<PaginatedList<ConsorcioDetailViewModel>> ObtenerConsorciosPaginados(Guid userId, int pageIndex, int pageSize);
    }
}
