using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.Models
{
    public class CustomRol
    {
        [Key]
        public int CustomRolId { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50)]
        public string NombreRol { get; set; } = null!;

        //navegador de usuarios
        public virtual ICollection<User> Usuarios { get; set; } = new HashSet<User>();

        public virtual ICollection<RolPermission> RolPermissions { get; set; } = new HashSet<RolPermission>();
    }

}