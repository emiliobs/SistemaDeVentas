using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeVentas.Areas.Usuarios.Models;
using SistemaDeVentas.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace SistemaDeVentas.Areas.Usuarios.Pages.Registrar
{
    public class RegistrarModel : PageModel
    {


        #region Atributtes
        private ListObject listObject = new ListObject();

        //private LUsuarios usuarios;
        #endregion

        #region Properties
        [BindProperty]
        public InputModelRegistrar InputModelRegistrar { get; set; }

        

        [Required]
        public string Role { get; set; }

        [Display(Name ="Lista de Roles.")]
        public List<SelectListItem> RoleList { get; set; }
        #endregion

        #region Contructors
        public RegistrarModel(RoleManager<IdentityRole> roleManager)
        {
            listObject.roleManager = roleManager;
            listObject.usuarios = new LUsuarios();
            listObject.usersRole = new UsersRoles();

        }
        #endregion

        public void OnGet()
        {

            RoleList = listObject.usersRole.GetRoles(listObject.roleManager).ToList();
                                     
           

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