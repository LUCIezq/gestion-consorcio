using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaParcial.Models.Consorcios.DTOs
{
    public class ConsorcioDetailViewModel
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Latitud { get; set; }
        public required string Longitud { get; set; }
    }
}