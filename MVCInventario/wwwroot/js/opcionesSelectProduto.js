var select = document.getElementById("selectProducto");
var div = document.getElementById("opcionProducto");
var btnBuscar = document.getElementById("btnBuscarPro");
select.addEventListener("change", () => {
    btnBuscar.value = "Buscar"
    if (select.value === "1") {
        div.innerHTML = '<input name="codigoString" type="text" asp-for="codigoString" id="search-input" placeholder="Ingrese el codigo">';
    } else if (select.value === "2") {
        div.innerHTML = '<input name="nombreString" type="text" asp-for="nombreString" id="search-input" placeholder="Ingrese el nombre del producto">';
    } else if (select.value === "3") {
        div.innerHTML = '<input name="categoriaString" type="text" asp-for="categoriaString" id="search-input" placeholder="Ingrese la categoría">';
    } else {
        div.innerHTML = "<h1>¿Eh?</h1>";
    }
});