using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.Models
{
    public class RolPermission
    {
        public int CustomRolId { get; set; }

        public string Role { get; set; } = null!;
        public CustomRol Name{ get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

    }
}