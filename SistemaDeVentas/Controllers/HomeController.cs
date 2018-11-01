using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SistemaDeVentas.Areas.Principal.Controllers;
using SistemaDeVentas.Library;
using SistemaDeVentas.Models;
using SistemaDeVentas.ViewModel;

namespace SistemaDeVentas.Controllers
{
    public class HomeController : Controller
    {
        private Usuarios usuarios;
        private readonly IServiceProvider serviceProvider;
        private SignInManager<IdentityUser> singInManager;
        //LoginViewModel loginViewModel;

        public HomeController(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.singInManager = signInManager;
            this.serviceProvider = serviceProvider;
            this.usuarios = new Usuarios(userManager, signInManager,roleManager);

         

        }
        public async Task<IActionResult> Index()
        {
            //await CreaateRole(this.serviceProvider);

            if (this.singInManager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(PrincipalController.Index),"Principal");
            }
            else
            {
                return View();
            }

          
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
          

            if (ModelState.IsValid)
            {
                List<object[]> listObject = await this.usuarios.UserLogin(model.Input.Email, model.Input.Password);

                object[] objects = listObject[0];   
                var identityError = (IdentityError)objects[0];
                model.ErrorMessage = identityError.Description;

                if (model.ErrorMessage.Equals("True"))
                {
                   
                    var data = JsonConvert.SerializeObject(objects[1]);
                    return RedirectToAction(nameof(PrincipalController.Index), "Principal");

                }
                else
                {
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task CreaateRole(IServiceProvider serviceProvider)
        {
            var messsage = string.Empty;
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string[] rolesName = { "Admin", "User" };
                foreach (var roles in rolesName)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roles);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roles));
                    }
                }

                var user = await userManager.FindByIdAsync("2ce00b2b-c914-466b-a002-fa4dff3e16d0");
                await userManager.AddToRoleAsync(user, "Admin");
                var user1 = await userManager.FindByIdAsync("129eb93e-d59b-4f61-9a26-3c0c49fbe1a4");
                await userManager.AddToRoleAsync(user1, "User");



            }
            catch (Exception ex)
            {

                messsage = ex.Message;
            }
        }
    }
}
