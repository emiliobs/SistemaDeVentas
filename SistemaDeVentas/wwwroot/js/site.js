/*Código principal*/

var principal = new Principal();

$().ready(() => {

    //aqui obtengo los parametros  de las url principal:
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
    //alert(URLactual);

    $('.sidenav').sidenav();
  
});