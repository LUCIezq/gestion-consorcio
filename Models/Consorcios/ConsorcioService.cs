
using PracticaParcial.Models.Consorcios.DTOs;


namespace PracticaParcial.Models.Consorcios
{
    public class ConsorcioService : IConsorcioService
    {
        public Task<Consorcio?> BuscarConsorcioPorDireccion(string calle, string ciudad, string provincia, string codigoPostal)
        {
            throw new NotImplementedException();
        }

        public Task<GuardarConsorcioResponse> GuardarConsorcio(CreateConsorcioViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}