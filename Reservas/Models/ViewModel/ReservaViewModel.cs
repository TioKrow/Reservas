namespace Reservas.Models.ViewModel
{
    public class ReservaViewModel
    {
        public int IdReserva { get; set; }
        public int IdUsr { get; set; }
        public DateTime FechaReserva { get; set; }
        public int IdModulo { get; set; }
        public int IdLab { get; set; }
        public string? Curso { get; set; }
        public string? Docente { get; set; }
        public DateTime? FinReserva { get; set; }
    }
    public class EditarReservaViewModel
    {
        public int IdReserva { get; set; }
        public int IdUsr { get; set; }
        public DateTime FechaReserva { get; set; }
        public int IdModulo { get; set; }
        public int IdLab { get; set; }
        public string? Curso { get; set; }
        public string? Docente { get; set; }
        public DateTime? FinReserva { get; set; }
    }
}
