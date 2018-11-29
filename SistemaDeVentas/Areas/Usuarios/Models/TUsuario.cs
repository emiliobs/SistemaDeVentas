namespace SistemaDeVentas.Areas.Usuarios.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    public class TUsuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NID { get; set; }
        public string Imagen { get; set; }
        public string IdUser { get; set; }

    }
}
