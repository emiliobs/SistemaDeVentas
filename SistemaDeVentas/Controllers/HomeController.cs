using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SistemaDeVentas.Models;
using SistemaDeVentas.ViewModel;

namespace SistemaDeVentas.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceProvider serviceProvider;
        public HomeController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
           
        }
        public async Task<IActionResult> Index()
        {
              //await CreaateRole(this.serviceProvider);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

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
