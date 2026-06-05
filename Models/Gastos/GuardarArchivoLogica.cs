namespace PracticaParcial.Models.Gastos
{
    public interface IGuardarArchivoLogica
    {

        Task<string?> GuardarArchivoAsync(IFormFile archivo);

    }
    public class GuardarArchivoLogica : IGuardarArchivoLogica
    {
        public static string Carpeta = "comprobantes";

        public async Task<string?> GuardarArchivoAsync(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return null;
            }

            try
            {
                string nuevaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Carpeta);

                if (!Directory.Exists(nuevaCarpeta))
                {
                    Directory.CreateDirectory(nuevaCarpeta);
                }

                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
                string rutaArchivo = Path.Combine(nuevaCarpeta, nombreArchivo);

                await using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                return nombreArchivo;
            }
            catch (Exception)
            {
     
                return null;
            }
        }
    }
}
