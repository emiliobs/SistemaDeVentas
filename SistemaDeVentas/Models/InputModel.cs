namespace SistemaDeVentas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    public class InputModel
    {
        [Required(ErrorMessage = "<font color='red'>El campo correo eléctonico es obligatiorio.</font>")]
        [EmailAddress(ErrorMessage = "<font color='red'>El correo eléctonico no es válido.</font>")]
        public string Email { get; set; }

        [Required(ErrorMessage = "<font color='red'>El campo contraseña es obligatiorio.</font>")]
        [DataType(DataType.Password)]
        [StringLength(100,ErrorMessage = "<font color='red'>El número de caracteres de {0} debe ser un máximo de {1} y un mínimo de {2}</font>",MinimumLength =6)]
        public string Password { get; set; }
    }
}
