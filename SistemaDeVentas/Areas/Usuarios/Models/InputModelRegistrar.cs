namespace SistemaDeVentas.Areas.Usuarios.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    public class InputModelRegistrar
    {
        [Key]
        public int RegistrarId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

         [Required]
        [DataType(DataType.Text)]
        public string Apellidos { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string DNI { get; set; }

        [Required]
         [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        [Display(Name = "Lista de Roles.")]
        public List<SelectListItem> RoleList { get; set; }
    }
}
