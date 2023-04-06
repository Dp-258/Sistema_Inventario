document.addEventListener('DOMContentLoaded', function() {
    
    // Obtener el botón hamburguesa y la barra lateral
const hamburgerButton = document.querySelector('.hamburger-button');
const sidebar = document.querySelector('.sidebar');

// Agregar un evento de clic al botón hamburguesa
    sidebar.classList.add('sidebar--hidden');

    // Agregar un evento de clic al botón hamburguesa
    hamburgerButton.addEventListener('click', () => {
        sidebar.classList.toggle('sidebar--hidden');
    });
});
function mostrarMenu() {
    var body = document.querySelector('body');
    body.classList.add('sidebar-visible');
}
function ocultarMenu() {
    var body = document.querySelector('body');
    body.classList.remove('sidebar-visible');
}
var hamburgerB = document.querySelector('#boton-nav');
hamburgerB.addEventListener('click', function () {
    var body = document.querySelector('body');
    if (body.classList.contains('sidebar-visible')) {
        ocultarMenu();
    } else {

        mostrarMenu();
    }
});
