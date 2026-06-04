
namespace PracticaParcial.Models.Consorcios.DTOs
{
    public class GuardarConsorcioResponse
    {
        public required bool Success { get; set; }
        public required string Message { get; set; }
        public int? IdConsorcio { get; set; }
    }
}