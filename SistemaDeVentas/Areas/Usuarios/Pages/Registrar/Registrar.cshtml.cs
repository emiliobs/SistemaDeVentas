using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeVentas.Areas.Usuarios.Models;
using SistemaDeVentas.Library;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace SistemaDeVentas.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {


        #region Atributtes
        private LUsuarios usuarios;
        #endregion

        #region Properties
       
        public InputModelRegistrar InputModelRegistrar { get; set; }
        #endregion


        public void OnGet()
        {

            //aqui obtengo el role del usurio que inciio session
            var roles = ClaimTypes.Role;
            //aqui busco un registro que este almacenado en la proepiedad(aqui solo obtengo el rol)
            var data = User.Claims.FirstOrDefault(u => u.Type.Equals(roles)).Value;
            ViewData["Roles"] = data;

            //usuarios = new LUsuarios();

            //ViewData["Roles"] = usuarios.UserData(HttpContext);
        }

        
    }

    
}