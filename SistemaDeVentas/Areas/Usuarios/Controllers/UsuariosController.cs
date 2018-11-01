namespace SistemaDeVentas.Areas.Usuarios.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SistemaDeVentas.Controllers;

    [Authorize]
   [Area("Usuarios")]
    public class UsuariosController : Controller
    {
        #region Attributtes
        private readonly SignInManager<IdentityUser> signInManager;
        #endregion

        #region Contructors
        public UsuariosController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;   
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> SessionClose()
        {
            //aqui elimino todos los usurios que iniciaron session:
            await this.signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
    }
}