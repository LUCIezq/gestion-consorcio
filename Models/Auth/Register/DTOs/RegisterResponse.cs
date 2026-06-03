using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaParcial.Models.Auth.Register.DTOs
{
    public class RegisterResponse
    {
        public required bool Success { get; set; }
        public required string Message { get; set; }
    }
}