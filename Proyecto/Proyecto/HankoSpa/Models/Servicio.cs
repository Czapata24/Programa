using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        [StringLength(100)]
        public string NombreServicio { get; set; }

        [Required(ErrorMessage = "La descripcion del servicio es obligatorio")]
        [StringLength(500)]
        public string DescripcionServicio { get; set; }

        //navegador de citas
        public virtual ICollection<Cita> Citas { get; set; }

    }
}