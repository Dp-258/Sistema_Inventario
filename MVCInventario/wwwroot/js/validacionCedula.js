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
    const provincia = Number(cedula.substring(0, 2));
    if (provincia < 1 || provincia > 24) {
        return false;
    }
    const ultimoDigito = Number(cedula.substring(9, 10));
    const coeficientes = [2, 1, 2, 1, 2, 1, 2, 1, 2];
    let suma = 0;
    for (let i = 0; i < coeficientes.length; i++) {
        const producto = coeficientes[i] * Number(cedula.charAt(i));
        suma += producto >= 10 ? producto - 9 : producto;
    }
    const digitoVerificador = 10 - (suma % 10);
    return ultimoDigito === digitoVerificador;
}