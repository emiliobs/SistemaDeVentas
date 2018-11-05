using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaDeVentas.Library;

namespace SistemaDeVentas.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {
       

        private LUsuarios usuarios;


        public void OnGet()
        {
            usuarios = new LUsuarios();

            ViewData["Roles"] = usuarios.UserData(HttpContext);
        }
    }
}