using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.Models
{
    public class Permission
    {
        [Key]
        public int PermisoId { get; set; }


        [Required(ErrorMessage = "El nombre del permiso es obligatorio")]
        [StringLength(50)]
        public string NombrePermiso { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "El modulo del permiso es obligatorio")]
        public string Module { get; set; } = null!;

        public ICollection<RolPermission> RolPermissions { get; set; } = new HashSet<RolPermission>();

    }

}