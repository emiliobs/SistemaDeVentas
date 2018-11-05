namespace SistemaDeVentas.Areas.Principal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SistemaDeVentas.Library;

    [Authorize]
    [Area("Principal")]
    public class PrincipalController : Controller
    {
        #region Attributtes
        private readonly LUsuarios usuarios;
        #endregion

        #region onstructs
        public PrincipalController()
        {
            this.usuarios = new LUsuarios();

        }
        #endregion

        public IActionResult Index()
        {
            ViewData["Roles"] = this.usuarios.UserData(HttpContext);
            return View();
        }
    }
}