using System;
using System.Collections.Generic;

namespace Reservas.Models
{
    public partial class TbLab
    {
        public TbLab()
        {
            TbReservas = new HashSet<TbReserva>();
        }

        public int IdLab { get; set; }
        public string NombreLab { get; set; } = null!;

        public virtual ICollection<TbReserva> TbReservas { get; set; }
    }
}
