namespace SistemaDeVentas.Library
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersRoles : ListObject
    {
        #region Constructors
        public UsersRoles()
        {
            this.userRolesList = new List<SelectListItem>();
        }
        #endregion

        #region Methods
        public async Task<List<SelectListItem>> GetRole(UserManager<IdentityUser> userManager, 
                                                  RoleManager<IdentityRole> roleManager, string ID)
        {
            var users = await userManager.FindByIdAsync(ID);
            var roles = await userManager.GetRolesAsync(users);

            //si es igual a cero no hay roles:
            if (roles.Count.Equals(0))
            {
                this.userRolesList.Add(new SelectListItem()
                {
                      Value = "0",
                      Text = "No Role",
                });

               
            }
            else
            {
                var roleUser = roleManager.Roles.Where(r => r.Name.Equals(roles[0])); 

                foreach (var role in roleUser)
                {
                    this.userRolesList.Add(new SelectListItem()
                    {
                        Value = role.Id,
                        Text = role.Name,
                    });
                }

              
            }

            return userRolesList;
        }

        //aqui contruyo una lista con todos los roles:
        public List<SelectListItem> GetRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = roleManager.Roles.ToList();
            roles.ForEach(item => {

                this.userRolesList.Add(new SelectListItem() {

                   Value = item.Id,
                   Text   = item.Name,

                });

            });

            return userRolesList;
        }

        #endregion
    }
}