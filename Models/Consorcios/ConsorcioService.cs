using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.Persistence.Consorcios;
using PracticaParcial.shared;

namespace PracticaParcial.Models.Consorcios
{
    public class ConsorcioService : IConsorcioService
    {
        private readonly IConsorcioRepository _consorcioRepository;

        public ConsorcioService(IConsorcioRepository consorcioRepository)
        {
            _consorcioRepository = consorcioRepository;
        }

        public async Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal)
        {
            return await _consorcioRepository.BuscarConsorcioPorDireccion(calle, ciudad, provincia, codigoPostal);
        }

        public async Task<EliminarConsorcioResponse> EliminarConsorcio(int id, Guid userId)
        {
            Consorcio? buscado = await _consorcioRepository.BuscarConsorcioPorId(id, userId);


            if (buscado == null)
            {
                return new EliminarConsorcioResponse
                {
                    Success = false,
                    Message = "No se encontró el consorcio a eliminar."
                };
            }

            await _consorcioRepository.EliminarConsorcio(buscado);
            return new EliminarConsorcioResponse
            {
                Success = true,
                Message = "Consorcio eliminado exitosamente."
            };
        }

        public async Task<GuardarConsorcioResponse> GuardarConsorcio(CreateConsorcioViewModel model, Guid userId)
        {

            Consorcio? buscado = await BuscarConsorcioPorDireccion(model.Calle, model.Ciudad, model.Provincia, model.CodigoPostal);

            if (buscado != null)
            {
                return new GuardarConsorcioResponse
                {
                    Success = false,
                    Message = "Ya existe un consorcio registrado con esa dirección."
                }
                ;
            }

            Consorcio nuevoConsorcio = new()
            {
                Nombre = model.Nombre,
                Calle = model.Calle,
                Ciudad = model.Ciudad,
                Provincia = model.Provincia,
                CodigoPostal = model.CodigoPostal,
                DiaVencimientoExpensas = model.DiaVencimientoExpensas,
                Latitud = model.Latitud,
                Longitud = model.Longitud,
                FechaCreacion = DateTime.UtcNow,
                UserId = userId
            };

            Consorcio consorcioGuardado = await _consorcioRepository.GuardarConsorcio(nuevoConsorcio);

            return new GuardarConsorcioResponse
            {
                Success = true,
                Message = "Consorcio guardado exitosamente.",
                IdConsorcio = consorcioGuardado.Id
            };
        }

        public async Task<Consorcio?> ObtenerConsorcioPorId(int id, Guid userId)
        {
            return await _consorcioRepository.BuscarConsorcioPorId(id, userId);
        }

        public async Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerConsorcios(Guid userId)
        {
            return await _consorcioRepository.ObtenerConsorcios(userId);
        }

        public async Task<PaginatedList<ConsorcioDetailViewModel>> ObtenerConsorciosPaginados(Guid userId, int pageIndex, int pageSize)
        {
            return await _consorcioRepository.ObtenerConsorciosPaginados(userId, pageIndex, pageSize);
        }

        public async Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerCoordenadas(Guid userId)
        {
            return await _consorcioRepository.ObtenerConsorcios(userId);
        }
    }
}
