using System;
using System.Collections.Generic;

namespace Reservas.Models
{
    public partial class TbRol
    {
        public TbRol()
        {
            TbUsrs = new HashSet<TbUsr>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;

        public virtual ICollection<TbUsr> TbUsrs { get; set; }
    }
}
