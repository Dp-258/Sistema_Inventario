function validarCampoTexto() {
    const campoTexto = document.getElementById("cedulatxt");
    const esValido = validarCampo(campoTexto.value);
    if (esValido) {
        if (!(validarCedula(campoTexto.value.toString()))) {
            campoTexto.setCustomValidity("¡Ingrese un número de cédula correcto!");
            campoTexto.setAttribute('aria-invalid', 'true');
            campoTexto.style.border = "1px solid red";
        }
        else {
            campoTexto.style.border = "1px solid #ced4da";
            campoTexto.setAttribute('aria-invalid', 'false');
        }
    } else {
        campoTexto.style.border = "1px solid #ced4da";
        campoTexto.setAttribute('aria-invalid', 'false');
    }
}

function validarCampoTextoDesp() {
    const campoTexto = document.getElementById("cedulatxt");
    const label = document.getElementById("erroreslbl");
    const esValido = validarCampo(campoTexto.value);
    if (esValido) {
        if (!(validarCedula(campoTexto.value.toString()))) {
            campoTexto.setCustomValidity("¡Ingrese un número de cédula correcto!");
            campoTexto.setAttribute('aria-invalid', 'true');
            campoTexto.style.border = "1px solid red";
            campoTexto.value = "";
            label.value = "¡Ingrese un número de cédula correcto!"
        }
        else {
            campoTexto.style.border = "1px solid #ced4da";
            campoTexto.setAttribute('aria-invalid', 'false');
        }
    } else {
        campoTexto.style.border = "1px solid #ced4da";
        campoTexto.setAttribute('aria-invalid', 'false');
    }
}

function validarCampo(campoTexto) {
    // Verificar que el campo no esté vacío
    if (campoTexto.length >= 1) {
        return true;
    }
    return false;
}

function validarCedula(cedula) {
    if (typeof (cedula) == 'string' && cedula.length == 10 && /^\d+$/.test(cedula)) {
        var digitos = cedula.split('').map(Number);
        var codigo_provincia = digitos[0] * 10 + digitos[1];

        //if (codigo_provincia >= 1 && (codigo_provincia <= 24 || codigo_provincia == 30) && digitos[2] < 6) {

        if (codigo_provincia >= 1 && (codigo_provincia <= 24 || codigo_provincia == 30)) {
            var digito_verificador = digitos.pop();

            var digito_calculado = digitos.reduce(
                function (valorPrevio, valorActual, indice) {
                    return valorPrevio - (valorActual * (2 - indice % 2)) % 9 - (valorActual == 9) * 9;
                }, 1000) % 10;
            return digito_calculado === digito_verificador;
        }
    }
    return false;
}