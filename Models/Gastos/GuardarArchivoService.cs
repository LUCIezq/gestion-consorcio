namespace PracticaParcial.Models.Gastos
{
    public interface IGuardarArchivoService
    {

        string? GuardarArchivo(IFormFile archivo);

    }
    public class GuardarArchivoService : IGuardarArchivoService
    {
        public static string Carpeta = "comprobantes";

        public string GuardarArchivo(IFormFile archivo)
        {
            string nombreArchivo = Guid.NewGuid() + Path.GetExtension(archivo.FileName);
            string carpetaDestino = Path.Combine("wwwroot", "comprobantes");

            
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string ruta = Path.Combine(carpetaDestino, nombreArchivo);

            using FileStream stream = new FileStream(ruta, FileMode.Create);
            archivo.CopyTo(stream);

            return nombreArchivo;
        }
    }
}
