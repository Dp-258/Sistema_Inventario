var select = document.getElementById("selectEntrada");
var div = document.getElementById("opcionEntrada");
var btnBuscar = document.getElementById("btnBuscar");
select.addEventListener("change", () => {
    btnBuscar.value = "Buscar"
    if (select.value === "1") {
        div.innerHTML = '<input name="CedString" type="text" asp-for="ProvNom" id="search-input" placeholder="Ingrese el proveedor">';
    } else if (select.value === "2") {
        div.innerHTML = '<input name="NomString" type="text" asp-for="NomString" id="search-input" placeholder="Ingrese el producto">';
    } else {
        div.innerHTML = "<h1>¿Eh?</h1>";
    }
});
