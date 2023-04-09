var select = document.getElementById("selectCliente");
var div = document.getElementById("opcionCliente");
var btnBuscar = document.getElementById("btnBuscar");
select.addEventListener("change", () => {
    btnBuscar.value = "Buscar"
    if (select.value === "1") {
        div.innerHTML = '<input name="CedString" type="text" asp-for="CedString" id="search-input" placeholder="Ingrese la cédula">';
    } else if (select.value === "2") {
        div.innerHTML = '<input name="NomString" type="text" asp-for="NomString" id="search-input" placeholder="Ingrese el nombre">';
    } else {
        div.innerHTML = "<h1>¿Eh?</h1>";
    }
});
