using System;
using System.Collections.Generic;

namespace PruebaTecnicaTareas.Models.Tables
{
    public partial class Tarea
    {
        public int Id { get; set; }
        public string Título { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVancimiento { get; set; }
        public string Estado { get; set; } = null!;
    }
}
