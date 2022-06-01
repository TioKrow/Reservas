using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Reservas.Models.ViewModel
{
    public class UsrViewModel
    {
        public int IdUsr { get; set; }
        [Required]
        [Display(Name ="Nombre")]
        public string NombreUsr { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string ApellidoUsr { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string EmailUsr { get; set; }
        [Required]
        [Display(Name = "Fono")]
        public int FonoUsr { get; set; }
        [Required]
        [Display(Name = "Rol")]
        public int RolUsr { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UsernameUsr { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordUsr { get; set; }

    }
    public class EditarUsrViewModel
    {
        public int IdUsr { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string NombreUsr { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        public string ApellidoUsr { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string EmailUsr { get; set; }
        [Required]
        [Display(Name = "Fono")]
        public int FonoUsr { get; set; }
        [Required]
        [Display(Name = "Rol")]
        public int RolUsr { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UsernameUsr { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordUsr { get; set; }

    }
}
