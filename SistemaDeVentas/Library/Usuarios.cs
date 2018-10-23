namespace SistemaDeVentas.Library
{
    using Microsoft.AspNetCore.Identity;
    using SistemaDeVentas.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class Usuarios:ListObject
    {
        #region Constructor
        public Usuarios()
        {

        }

        public Usuarios(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.usersRole = new UsersRoles();

        }

        public Usuarios(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, 
                       RoleManager<IdentityRole> roleManager)            
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.usersRole = new UsersRoles();

        }
        #endregion

        #region Methods
        internal async Task<List<Object[]>> UserLogin(string email, string password)
        {
            try
            {
                var result = await this.signInManager.PasswordSignInAsync(email, password, false,
                                                                         lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var appUser = this.userManager.Users.Where(u => u.Email.Equals(email)).ToList();
                    this.userRolesList = await this.usersRole.GetRole(this.userManager, this.roleManager,
                                                                      appUser[0].Id);

                    this.usersData = new UserData()
                    {
                        Id = appUser[0].Id,
                        Role = this.userRolesList[0].Text,
                        UserName = appUser[0].UserName,
                    };

                    code = "0";
                    description = result.Succeeded.ToString();
                }
                else
                {
                    this.code = "1";
                    this.description = "Correo o Contraseña Invalidos";
                }


            }
            catch (Exception ex)
            {

                code = "2";
                description = ex.Message;
            }


            this.identityError = new IdentityError()
            {
             Code = code,
             Description = description,
            };

            object[] data = { this.identityError,  this.usersData};
            this.dataList.Add(data);

            return dataList;
           
        }
        #endregion
    }
}
