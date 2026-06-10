using PracticaParcial.Models.Unidades;
using PracticaParcial.Models.Users;

namespace PracticaParcial.Models.Consorcios
{
    public class Consorcio
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Calle { get; set; }
        public required string Ciudad { get; set; }
        public required string Provincia { get; set; }
        public required string CodigoPostal { get; set; }
        public required int DiaVencimientoExpensas { get; set; }
        public required double Latitud { get; set; }
        public required double Longitud { get; set; }
        public required DateTime FechaCreacion { get; set; }

        public required Guid UserId { get; set; }
        public User? User { get; set; }

        public List<Unidad>? Unidades { get; set; }
    }
}