namespace PracticaParcial.Models.Consorcio
{
    public class CreateConsorcioViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public int ProvinciaId { get; set; }
        public int CiudadId { get; set; }
        public string Calle { get; set; } = string.Empty;
        public string Altura { get; set; } = string.Empty;
        public int DiaVencimientoExpensas { get; set; }
    }
}
