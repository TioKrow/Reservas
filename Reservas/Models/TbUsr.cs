using System;
using System.Collections.Generic;

namespace Reservas.Models
{
    public partial class TbUsr
    {
        public int IdUsr { get; set; }
        public string NombreUsr { get; set; } = null!;
        public string ApellidoUsr { get; set; } = null!;
        public string EmailUsr { get; set; } = null!;
        public int FonoUsr { get; set; }
        public int RolUsr { get; set; }

        public virtual TbRol RolUsrNavigation { get; set; } = null!;
    }
}
