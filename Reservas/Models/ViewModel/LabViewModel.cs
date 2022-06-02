using System.ComponentModel.DataAnnotations;

namespace Reservas.Models.ViewModel
{
    public class LabViewModel
    {
        public int IdLab { get; set; }
        [Required]
        [Display(Name = "Nombre Lab")]
        public string NombreLab { get; set; } = null!;
    }
    public class EditarLabViewModel
    {
        public int IdLab { get; set; }
        [Required]
        [Display(Name = "Nombre Lab")]
        public string NombreLab { get; set; } = null!;
    }
}
