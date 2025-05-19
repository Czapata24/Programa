using System.ComponentModel.DataAnnotations;

namespace HankoSpa.DTOs
{
    public class CitaDTO
    {
        public int CitaId { get; set; }

        [Required(ErrorMessage = "La fecha de la cita es obligatoria")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha inválido")]
        public DateTime FechaCita { get; set; }

        [Required(ErrorMessage = "La hora de la cita es obligatoria")]
        [DataType(DataType.Time, ErrorMessage = "Formato de hora inválido")]
        public TimeSpan HoraCita { get; set; }

        [Required(ErrorMessage = "El estado de la cita es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string EstadoCita { get; set; }

        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public String UsuarioID { get; set; }

        // Relación muchos a muchos con servicios
        [Required(ErrorMessage = "Debe seleccionar al menos un servicio")]
        public int ServicioID { get; set; }

    }
}
