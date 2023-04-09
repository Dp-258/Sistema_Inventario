var select = document.getElementById("selectProveedor");
var div = document.getElementById("opcionProveedor");
var btnBuscar = document.getElementById("btnBuscarPro");
select.addEventListener("change", () => {
    btnBuscar.value = "Buscar"
    if (select.value === "1") {
        div.innerHTML = '<input name="CedString" type="text" asp-for="CedString" id="search-input" placeholder="Ingrese la cédula">';
    } else if (select.value === "2") {
        div.innerHTML = '<input name="NomString" type="text" asp-for="NomString" id="search-input" placeholder="Ingrese el nombre">';
    } else if (select.value === "3") {
        div.innerHTML = '<input name="DirString" type="text" asp-for="DirString" id="search-input" placeholder="Ingrese la dirección">';
    } else if (select.value === "4") {
        div.innerHTML = '<input name="EmailString" type="text" asp-for="EmailString" id="search-input" placeholder="Ingrese el email">';
    } else if (select.value === "5") {
        div.innerHTML = '<input name="CiuString" type="text" asp-for="CiuString" id="search-input" placeholder="Ingrese la ciudad">';
    } else {
        div.innerHTML = "<h1>¿Eh?</h1>";
    }
});
