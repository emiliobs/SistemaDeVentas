class Principal {

    constructor() {


    }


    //metodo
    userLink(URLactual) {

        //alert(URLactual);

        if (URLactual === "/Principal" || URLactual === "/Principal/") {

            document.getElementById("enlace1").classList.add('active');

        }
        if (URLactual === "/Usuarios" || URLactual === "/Usuarios/" || URLactual === "/Usuarios/Registrar/Registrar") {

            document.getElementById("enlace2").classList.add('active');


        }
        if (URLactual === "/Usuarios/Registrar/Registrar/" || URLactual === "/Usuarios/Registrar/Registrar") {

            document.getElementById("enlace2").classList.add('active');

        }

    }
}
