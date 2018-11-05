namespace SistemaDeVentas.Areas.Usuarios.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            // ViewData["Roles"] = this.usuarios.UserData(HttpContext);
            ViewData["Roles"] = this.usuarios.UserData(HttpContext);

            return View();
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