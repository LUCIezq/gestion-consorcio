using PracticaParcial.Models.Users;

namespace PracticaParcial.Models.Notificaciones
{
    public class Notificacion{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateOnly FechaDeCreacion { get; set; }
        public DateOnly FechaDeEnvio { get; set; }

        public Consorcios.Consorcio consorcio { get; set; }
    }
}
