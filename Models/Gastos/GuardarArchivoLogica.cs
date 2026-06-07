namespace PracticaParcial.Models.Gastos
{
    public interface IGuardarArchivoLogica
    {

        string? GuardarArchivo(IFormFile archivo);

    }
    public class GuardarArchivoLogica : IGuardarArchivoLogica
    {
        public static string Carpeta = "comprobantes";

        public string GuardarArchivo(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return null;
            }

            
                string nuevaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Carpeta);

                if (!Directory.Exists(nuevaCarpeta))
                {
                    Directory.CreateDirectory(nuevaCarpeta);
                }

                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                string rutaArchivo = Path.Combine(nuevaCarpeta, nombreArchivo);

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return nombreArchivo;
          
        }
    }
}
