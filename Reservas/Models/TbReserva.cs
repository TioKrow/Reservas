using System;
using System.Collections.Generic;

namespace Reservas.Models
{
    public partial class TbReserva
    {
        public int IdReserva { get; set; }
        public int IdUsr { get; set; }
        public DateTime FechaReserva { get; set; }
        public int IdModulo { get; set; }
        public int IdLab { get; set; }
        public string? Curso { get; set; }
        public string? Docente { get; set; }
        public DateTime? FinReserva { get; set; }

        public virtual TbLab IdLabNavigation { get; set; } = null!;
        public virtual TbModulo IdModuloNavigation { get; set; } = null!;
        public virtual TbUsr IdUsrNavigation { get; set; } = null!;
    }
}
