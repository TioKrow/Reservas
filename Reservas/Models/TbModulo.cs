using System;
using System.Collections.Generic;

namespace Reservas.Models
{
    public partial class TbModulo
    {
        public TbModulo()
        {
            TbReservas = new HashSet<TbReserva>();
        }

        public int IdModulo { get; set; }
        public TimeSpan InicioMod { get; set; }
        public TimeSpan FinMod { get; set; }

        public virtual ICollection<TbReserva> TbReservas { get; set; }
    }
}
