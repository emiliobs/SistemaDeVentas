/*Código principal*/

var principal = new Principal();

/*CODIGO DE USUARIOS*/
var usuarios = new Usurios();
var imageUser = (evt) => {

    usuarios.archivo(evt,"imagenRegistrar");

};


$().ready(() => {

    //aqui obtengo los parametros  de las url principal:
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
    //alert(URLactual);

   // $('.sidenav').sidenav();
    M.AutoInit();

    if (URLactual === "/Usuarios/Registrar/Registrar/" || URLactual === "/Usuarios/Registrar/Registrar") {

        document.getElementById('files').addEventListener('change', imageUser, false);

    }
  
});