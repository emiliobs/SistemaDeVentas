using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeVentas.Areas.Usuarios.Models;
using SistemaDeVentas.Library;
using System.ComponentModel;

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
            usuarios = new LUsuarios();

            ViewData["Roles"] = usuarios.UserData(HttpContext);
        }

        
    }

    
}