using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeVentas.Areas.Usuarios.Models;
using SistemaDeVentas.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public IFormFile AvatarImage { get; set; }

        [Display(Name = "Lista de Roles.")]
        public List<SelectListItem> RoleList { get; set; }
        #endregion

        #region Contructors
        public RegistrarModel(RoleManager<IdentityRole> roleManager, IHostingEnvironment environment)
        {
            listObject.environment = environment;
            listObject.roleManager = roleManager;
            listObject.uploadImage = new UploadImage();
            listObject.usuarios = new LUsuarios();
            listObject.usersRole = new UsersRoles();

        }
        #endregion

        #region Methods
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //aqui subo la foto ya con avatar si es nula cuando crea el nuevo usurio:
                await GuardarImage();
                //var imageName = this.InputModelRegistrar.Email + ".png";

                //var filePath = Path.Combine(listObject.environment.ContentRootPath, "wwwroot/images/foto", imageName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await AvatarImage.CopyToAsync(stream);
                //}



            }
            catch (Exception ex)
            {



            }


            return Page();
        }

        private async Task GuardarImage()
        {
            try
            {
                var imageName = InputModelRegistrar.Email + ".png";

                await listObject.uploadImage.CopiarImagenAsync(AvatarImage, imageName, listObject.environment);
            }
            catch (Exception)
            {

                throw;
            }
        }         
        #endregion

    }


}