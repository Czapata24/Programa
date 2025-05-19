using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; } = null!;

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Document")]
        [Required(ErrorMessage = "El documento es obligatorio")]
        public string Document { get; set; } = null!;

        public string Name { get; set; } = null!;

        public virtual ICollection<Cita>? Citas { get; set; }

        public int CustomRolId { get; set; }

        public virtual CustomRol? CustomRol { get; set; }

        

    }
}
