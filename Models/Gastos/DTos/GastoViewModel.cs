using System.ComponentModel.DataAnnotations;

namespace PracticaParcial.Models.Gastos.DTos
{
    public class GastoViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El  consorcio es obligatorio.")]
        public int IdConsorcio { get; set; }

        [Required(ErrorMessage = "Seleccione un tipo de gasto")]
        public int IdTipoGasto { get; set; }

        [Required(ErrorMessage = "El nombre del gasto es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [MaxLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha del gasto es obligatoria.")]
        public DateOnly FechaGasto { get; set; }

        [Required(ErrorMessage = "El año de la expensa es obligatorio.")]
        [Range(2025, 2030, ErrorMessage = "El año de la expensa debe estar entre 2025 y 2030.")]
        public int AnioExpensa { get; set; }

        [Required(ErrorMessage = "El mes de la expensa es obligatorio.")]
        public int MesExpensa { get; set; }

        public IFormFile? ArchivoComprobante { get; set; }

        public string? ArchivoComprobanteGuardado { get; set; }

        [Required(ErrorMessage = "El monto del gasto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto del gasto debe ser un valor positivo.")]
        public decimal Monto { get; set; }

        public TipoGasto? TipoGasto { get; set; }


        public string? NombreTipoGasto { get; set; }

        public Gasto ToEntity()
        {
            return new Gasto
            {
                Id = this.Id,
                IdConsorcio = this.IdConsorcio,
                IdTipoGasto = this.IdTipoGasto,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                FechaGasto = this.FechaGasto,
                AnioExpensa = this.AnioExpensa,
                MesExpensa = this.MesExpensa,
                ArchivoComprobante = this.ArchivoComprobanteGuardado,
                Monto = this.Monto
            };
        }

        public static GastoViewModel FromEntity(Gasto gasto)
        {
            return new GastoViewModel
            {
                Id = gasto.Id,
                IdConsorcio = gasto.IdConsorcio,
                IdTipoGasto = gasto.IdTipoGasto,
                Nombre = gasto.Nombre,
                Descripcion = gasto.Descripcion,
                FechaGasto = gasto.FechaGasto,
                AnioExpensa = gasto.AnioExpensa,
                MesExpensa = gasto.MesExpensa,
                ArchivoComprobanteGuardado = gasto.ArchivoComprobante,
                Monto = gasto.Monto,
                NombreTipoGasto = gasto.TipoGasto?.Nombre
            };
        }
    }

}