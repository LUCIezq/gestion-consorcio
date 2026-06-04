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

        public async Task<GuardarConsorcioResponse> GuardarConsorcio(CreateConsorcioViewModel model)
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
                FechaCreacion = DateTime.UtcNow
            };

            Consorcio consorcioGuardado = await _consorcioRepository.GuardarConsorcio(nuevoConsorcio);

            return new GuardarConsorcioResponse
            {
                Success = true,
                Message = "Consorcio guardado exitosamente.",
                IdConsorcio = consorcioGuardado.Id
            };
        }

        public async Task<IEnumerable<ConsorcioDetailViewModel>> ObtenerCoordenadas()
        {
            return await _consorcioRepository.ObtenerCoordenadas();
        }
    }
}