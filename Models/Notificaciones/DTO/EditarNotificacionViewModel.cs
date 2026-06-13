using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Notificaciones.DTO
{
    public class EditarNotificacionViewModel
    {
        public int IdConsorcio { get; set; }
        public int IdNotificacion { get; set; }
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]

        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }

        public static EditarNotificacionViewModel toViewModel(Notificacion noti)
        {
            return new EditarNotificacionViewModel
            {
                IdNotificacion = noti.Id,
                Titulo = noti.Titulo,
                Descripcion = noti.Descripcion,
                IdConsorcio = noti.consorcio.Id,
                Fecha = noti.FechaDeCreacion ,
            };
        }
    }
}