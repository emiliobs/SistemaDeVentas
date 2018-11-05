namespace SistemaDeVentas.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    public class LoginViewModel
    {
        #region Properties
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructors
        //public LoginViewModel()
        //{
        //    Input.Email = "emilio@gmail.com";
        //    Input.Password = "Eabs123.";
        //}
        #endregion

        #region Methods

        #endregion
    }
}
