document.addEventListener('DOMContentLoaded', function() {
    
    // Obtener el botón hamburguesa y la barra lateral
const hamburgerButton = document.querySelector('.hamburger-button');
const sidebar = document.querySelector('.sidebar');

// Agregar un evento de clic al botón hamburguesa
hamburgerButton.addEventListener('click', () => {
  sidebar.classList.toggle('sidebar--hidden');
});

  });
