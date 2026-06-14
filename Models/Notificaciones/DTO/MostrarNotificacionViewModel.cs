using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Notificaciones.DTO
{
    public class MostrarNotificacionesViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateOnly FechaDeCreacion { get; set; }
        public DateOnly? FechaDeEnvio { get; set; }

        public static List<MostrarNotificacionesViewModel> ObtenerListaDeViewModel(List<Notificacion> notificaciones)
        {
            List<MostrarNotificacionesViewModel> list = new List<MostrarNotificacionesViewModel>();

            foreach (var n in notificaciones)
            {
                string descripcionCorta = n.Descripcion;

                if (descripcionCorta.Length>50)
                {
                    descripcionCorta = n.Descripcion.Substring(0, 25) + "...";
                }

                MostrarNotificacionesViewModel x = new MostrarNotificacionesViewModel()
                {
                    Id=n.Id,
                    Titulo=n.Titulo,
                    Descripcion=descripcionCorta,
                    FechaDeCreacion=n.FechaDeCreacion,
                    FechaDeEnvio=n.FechaDeEnvio
                };

                list.Add(x);
            }

            return list;
        }
    }
}