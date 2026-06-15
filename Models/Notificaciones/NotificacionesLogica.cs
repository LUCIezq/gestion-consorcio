using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PracticaParcial.Models.Consorcios;
using PracticaParcial.Models.Unidades;
using PracticaParcial.Models.Notificaciones.DTO;
using PracticaParcial.Persistence;
using System.Diagnostics;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace PracticaParcial.Models.Notificaciones
{
    public interface INotificacionesLogica
    {
        List<Notificacion> ObtenerNotificaciones(int IdConsorcio);
        void AgregarNotificacion(Notificacion nueva);

        Task EnviarNotificacion(Notificacion notificacion);

        Notificacion ObtenerNotificacionPorId(int idNotificacion);
        void EliminarNotificacion(Notificacion notificacion);
        void ActualizarNotificacion(Notificacion notificacion, EditarNotificacionViewModel notiModel);
    }

    public class NotificacionesLogica : INotificacionesLogica
    {

        private readonly UnidadDbContext db;

        public NotificacionesLogica(UnidadDbContext db)
        {
            this.db = db;
        }

        public List<Notificacion> ObtenerNotificaciones(int IdConsorcio)
        {

            List<Notificacion> notificaciones = db.Notificaciones.
                                        Where(n => n.consorcio.Id == IdConsorcio)
                                        .ToList(); 

            return notificaciones;
        }

        public void AgregarNotificacion(Notificacion nueva)
        {
            db.Notificaciones.Add(nueva);
            db.SaveChanges();
        }

        public async Task EnviarNotificacion(Notificacion notificacion)
        {
            List<string> mailPropietarios = db.Unidades
                .Where(u => u.Consorcio.Id == notificacion.consorcio.Id)
                .Select(u=>u.EmailPropietario)  
                .ToList();

            foreach (string mailPropietario in mailPropietarios)
            {
                await EnviarNotificacionPorSMTP(mailPropietario, notificacion.Titulo, notificacion.Descripcion);
            }


            notificacion.FechaDeEnvio = DateOnly.FromDateTime(DateTime.Now);
            db.Notificaciones.Update(notificacion);
            db.SaveChanges();
        }

        public Notificacion ObtenerNotificacionPorId(int idNotificacion)
        {
            return db.Notificaciones
                .Include(n => n.consorcio)
                .FirstOrDefault(n => n.Id == idNotificacion);
        }

        public void EliminarNotificacion(Notificacion notificacion)
        {
            db.Notificaciones.Remove(notificacion);
            db.SaveChanges();
        }

        public void ActualizarNotificacion(Notificacion notificacion, EditarNotificacionViewModel notiModel)
        {

            notificacion.Titulo = notiModel.Titulo;
            notificacion.Descripcion = notiModel.Descripcion;
            db.Notificaciones.Update(notificacion);
            db.SaveChanges();
        }
        public async Task EnviarNotificacionPorSMTP(string destino, string titulo, string descripcion)
        {
            var email = new MimeMessage();
            string mailDelTp = "tpconsorciospw3@gmail.com";
            string claveDeAplicacion = "twsm qwyn zzlc elwf";

            email.From.Add(
                new MailboxAddress(
                    "Mi Aplicación",
                    mailDelTp));

            email.To.Add(
                MailboxAddress.Parse(destino));

            email.Subject = titulo;

            email.Body = new TextPart("plain")
            {
                Text = descripcion
            };

            try
            {
                using var smtp = new SmtpClient();

                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await smtp.ConnectAsync(
                    "smtp.gmail.com",
                    587,
                    SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(
                    mailDelTp,
                    claveDeAplicacion);

                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
