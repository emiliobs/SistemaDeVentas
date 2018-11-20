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

        
        public IFormFile AvatarImage { get; set; }

       

        [TempData]
        public string ErrorMessage { get; set; }
        #endregion

        #region Contructors
        public RegistrarModel(RoleManager<IdentityRole> roleManager, IHostingEnvironment environment, UserManager<IdentityUser> userManager)
        {
            listObject.environment = environment;
            listObject.userManager = userManager;
            listObject.roleManager = roleManager;
            listObject.uploadImage = new UploadImage();
            listObject.usuarios = new LUsuarios();
            listObject.usersRole = new UsersRoles();
            listObject.userRolesList = new List<SelectListItem>();

        }
        #endregion

        #region Methods
        public void OnGet()
        {

            InputModelRegistrar = new InputModelRegistrar
            {

                RoleList = listObject.usersRole.GetRoles(listObject.roleManager).ToList(),

        };

            



            //aqui obtengo el role del usurio que inciio session
            var role = ClaimTypes.Role;
            //aqui busco un registro que este almacenado en la proepiedad(aqui solo obtengo el rol)
            var data = User.Claims.FirstOrDefault(u => u.Type.Equals(role)).Value;
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
            
                listObject.userRolesList.Add(new SelectListItem
                {
                    Text = InputModelRegistrar.Role,
                });


                var userList = listObject.userManager.Users.Where(u => u.Email.Equals(InputModelRegistrar.Email)).ToList();
                if (userList.Count.Equals(0))
                {
                    var imageName = InputModelRegistrar.Email + ".png";

                    var user = new IdentityUser()
                    {
                        UserName = InputModelRegistrar.Email,
                        Email = InputModelRegistrar.Email,
                        PhoneNumber = InputModelRegistrar.Telefono,
                    };

                    //aqui salvo a la bd:
                    var result = await listObject.userManager.CreateAsync(user, InputModelRegistrar.Password);
                    if (result.Succeeded)
                    {
                       
                    }   
                    else
                    {
                        var query = from u in result.Errors select u;
                        foreach (var usuarioResult in query)
                        {
                            ErrorMessage = usuarioResult.Description;
                            InputModelRegistrar.RoleList = listObject.userRolesList;
                        }
                    }
                    await listObject.uploadImage.CopiarImagenAsync(AvatarImage, imageName, listObject.environment);
                }
                else
                {
                    ErrorMessage = $"El {InputModelRegistrar.Email} ya esta registrado";
                    InputModelRegistrar.RoleList = listObject.userRolesList;
                }

            
            }
            catch (Exception ex)
            {

                this.ErrorMessage = ex.Message;
                InputModelRegistrar.RoleList = listObject.userRolesList;
            }
        }         
        #endregion

    }


}