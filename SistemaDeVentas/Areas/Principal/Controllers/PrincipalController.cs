namespace SistemaDeVentas.Areas.Principal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SistemaDeVentas.Controllers;
    using SistemaDeVentas.Library;

    [Authorize]
    [Area("Principal")]
    public class PrincipalController : Controller
    {
        #region Attributtes
        private readonly LUsuarios usuarios;
        private SignInManager<IdentityUser> signInManager;
        #endregion

        #region onstructs
        public PrincipalController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.usuarios = new LUsuarios();

        }
        #endregion

        public IActionResult Index()
        {
            //verifico si un usurio ha iniciado session:
            if (this.signInManager.IsSignedIn(User))
            {
                ViewData["Roles"] = this.usuarios.UserData(HttpContext);

                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }



        }
    }
}