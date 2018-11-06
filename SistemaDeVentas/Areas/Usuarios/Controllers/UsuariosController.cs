namespace SistemaDeVentas.Areas.Usuarios.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SistemaDeVentas.Controllers;
    using SistemaDeVentas.Library;

    [Authorize]
   [Area("Usuarios")]
    public class UsuariosController : Controller
    {
        #region Attributtes
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly LUsuarios usuarios;
        #endregion

        #region Contructors
        public UsuariosController(SignInManager<IdentityUser> signInManager)
        {
            this.usuarios = new LUsuarios();

            this.signInManager = signInManager;   
        }
        #endregion

        public IActionResult Index()
        {
            //verifico si un usurio ha iniciado session:
            if (this.signInManager.IsSignedIn(User))
            {
                //aqui obtengo el role del usurio que inciio session
                var roles = ClaimTypes.Role;
                //aqui busco un registro que este almacenado en la proepiedad(aqui solo obtengo el rol)
                var data = User.Claims.FirstOrDefault(u => u.Type.Equals(roles)).Value;
                ViewData["Roles"] = data;
                //ViewData["Roles"] = this.usuarios.UserData(HttpContext);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        public async Task<IActionResult> SessionClose()
        {

            //aqui ciero la session:
            HttpContext.Session.Remove("User");
            //aqui elimino todos los usurios que iniciaron session:
            await this.signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}