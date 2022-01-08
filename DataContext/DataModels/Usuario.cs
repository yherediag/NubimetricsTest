using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Habilitado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificado { get; set; }
    }
}
