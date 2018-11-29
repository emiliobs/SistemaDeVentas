using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeVentas.Areas.Usuarios.Controllers;
using SistemaDeVentas.Areas.Usuarios.Models;
using SistemaDeVentas.Data;
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
        public RegistrarModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IHostingEnvironment environment, UserManager<IdentityUser> userManager)
        {
            listObject.environment = environment;
            listObject.userManager = userManager;
            listObject.roleManager = roleManager;
            listObject.db = db;

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
                        await listObject.userManager.AddToRoleAsync(user, InputModelRegistrar.Role);
                        //aqui obtengo todos los usurio que esten registrado:
                        var listUser = listObject.userManager.Users.ToList();
                        var count = listUser.Count();
                        count--;
                        var usuario = new TUsuario()
                        {

                            Nombre = InputModelRegistrar.Nombre,
                            Apellidos = InputModelRegistrar.Apellidos,
                            IdUser = listUser[count].Id,
                            Imagen = InputModelRegistrar.Email,
                            NID = InputModelRegistrar.DNI,

                        };

                        await listObject.db.AddAsync(usuario);
                        await listObject.db.SaveChangesAsync();
                        // await listObject.uploadImage.CopiarImagenAsync(AvatarImage,imageName, listObject.environment);
                        await listObject.uploadImage.CopiarImagenAsync(AvatarImage, imageName, listObject.environment);

                        return RedirectToAction(nameof(UsuariosController.Index), "Usuarios");
                    }
                    else
                    {
                        //var query = from u in result.Errors select u;
                        foreach (var usuarioResult in result.Errors)
                        {
                            ErrorMessage = usuarioResult.Description;
                            InputModelRegistrar.RoleList = listObject.userRolesList;
                        }
                    }

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
                  

            return Page();
        }

      
        #endregion

    }


}