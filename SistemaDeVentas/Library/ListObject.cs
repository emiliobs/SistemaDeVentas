namespace SistemaDeVentas.Library
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SistemaDeVentas.Data;
    using SistemaDeVentas.Models;
    using System.Collections.Generic;
    public class ListObject
    {
        #region Attributes
        public string description, code;
        public List<SelectListItem> userRolesList;
        public LUsuarios usuarios;
        public UserData usersData;
        public UsersRoles usersRole;
        public IdentityError identityError;
        public ApplicationDbContext db;
        public RoleManager<IdentityRole> roleManager;
        public UserManager<IdentityUser> userManager;
        public SignInManager<IdentityUser> signInManager;
        public List<object[]> dataList = new List<object[]>();
        #endregion
    }
}
