namespace PracticaParcial.Models.Consorcios
{
    public class Consorcio
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Calle { get; set; }
        public required string Ciudad { get; set; }
        public required string Provincia { get; set; }
        public required string CodigoPostal { get; set; }
        public required int DiaVencimientoExpensas { get; set; }
        public required double Latitud { get; set; }
        public required double Longitud { get; set; }
        public required DateTime FechaCreacion { get; set; }
    }
}