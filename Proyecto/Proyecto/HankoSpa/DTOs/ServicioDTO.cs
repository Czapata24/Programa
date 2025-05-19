using System.ComponentModel.DataAnnotations;

namespace HankoSpa.DTOs
{
    public class ServiceDTO
    {
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string NombreServicio { get; set; }

        [Required(ErrorMessage = "La descripción del servicio es obligatoria")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string DescripcionServicio { get; set; }
    }
}
