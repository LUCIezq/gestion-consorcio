using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Notificaciones.DTO
{
    public class EditarNotificacionViewModel
    {
        public int IdConsorcio { get; set; }
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]

        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        public Notificacion toEntity()
        {
            return new Notificacion
            {
                Titulo = this.Titulo,
                Descripcion = this.Descripcion,
            };
        }
    }
}