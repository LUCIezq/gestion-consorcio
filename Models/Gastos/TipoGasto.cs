using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace PracticaParcial.Models.Gastos
{
    public class TipoGasto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    
    }
}
