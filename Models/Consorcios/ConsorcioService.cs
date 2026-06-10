using PracticaParcial.Models.Consorcios.DTOs;
using PracticaParcial.Persistence.Consorcios;


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

        public async Task<EliminarConsorcioResponse> EliminarConsorcio(int id)
        {
            Consorcio? buscado = await _consorcioRepository.BuscarConsorcioPorId(id);

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
            //deberia validar los campos que me llegan -> No todo siempre puede llegar desde un dto, podria llegar a este metodo desde postman por ejemplo, entonces no puedo confiar en que el modelo siempre va a ser correcto, por eso es importante validar los campos antes de hacer cualquier operacion con ellos.

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

        public async Task<Consorcio?> ObtenerConsorcioPorId(int id)
        {
            return await _consorcioRepository.BuscarConsorcioPorId(id);
        }

        public async Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerConsorcios()
        {
            return await _consorcioRepository.ObtenerConsorcios();
        }

        public async Task<IEnumerable<ConsorcioCoordenadaViewModel>> ObtenerCoordenadas()
        {
            return await _consorcioRepository.ObtenerCoordenadas();
        }
    }
}