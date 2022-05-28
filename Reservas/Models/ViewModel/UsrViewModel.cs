using System.ComponentModel.DataAnnotations;

namespace Reservas.Models.ViewModel
{
    public class UsrViewModel
    {
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
    }
}
