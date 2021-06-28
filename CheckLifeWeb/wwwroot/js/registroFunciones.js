function Validar() {
    var contrasena = document.getElementById('Password');
    var contrasenaVerificar = document.getElementById('VerificarPassword');
    if (contrasena.value != contrasenaVerificar.value) {
        document.getElementById('AlertError').classList.remove('desactivado');
        return false;
    }       
    else {
        document.getElementById('AlertError').classList.remove('desactivado');
        return true;
    }
     
}


