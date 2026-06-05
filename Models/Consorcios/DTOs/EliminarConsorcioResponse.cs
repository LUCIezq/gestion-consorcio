using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaParcial.Models.Consorcios.DTOs
{
    public class EliminarConsorcioResponse
    {
        public required bool Success { get; set; }
        public required string Message { get; set; }
    }
}