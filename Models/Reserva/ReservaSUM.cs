using PracticaParcial.Models.Reserva.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PracticaParcial.Models.Unidades;

namespace PracticaParcial.Models.Reserva;

public class ReservaSUM
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateOnly Fecha { get; set; }
    [Required]
    public Turno Turno { get; set; }
    [Required]
    public int UnidadId { get; set; }
    [ForeignKey(nameof(UnidadId))]
    [ValidateNever]
    public Unidad Unidad { get; set; }
    [StringLength(maximumLength: 500)]
    public string? Observaciones { get; set; }
    public bool EntregoCorrectamente { get; set; }

}
