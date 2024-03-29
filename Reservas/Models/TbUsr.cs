﻿using System;
using System.Collections.Generic;

namespace Reservas.Models
{
    public partial class TbUsr
    {
        public TbUsr()
        {
            TbReservas = new HashSet<TbReserva>();
        }

        public int IdUsr { get; set; }
        public string NombreUsr { get; set; } = null!;
        public string ApellidoUsr { get; set; } = null!;
        public string EmailUsr { get; set; } = null!;
        public int FonoUsr { get; set; }
        public int RolUsr { get; set; }
        public string UsernameUsr { get; set; } = null!;
        public string PasswordUsr { get; set; } = null!;

        public virtual TbRol RolUsrNavigation { get; set; } = null!;
        public virtual ICollection<TbReserva> TbReservas { get; set; }
    }
}
