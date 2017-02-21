// Declara variables globales
var iterador = parseInt(1); 
var clave = "clave";
var valor = "valor";
var sinFormato = "";

// Metodo que agrega los componente al formulario Form-Data 
function agregar_Click() {

    // Crea los div´s tag necesarios para agregar las fila al panel
    var row = document.createElement("div");    
    var rowLabelCla = document.createElement("div");
    var rowTextCla = document.createElement("div");
    var rowLabelVa = document.createElement("div");
    var rowTextVa = document.createElement("div");

    // Crea los br  tag necesarios para agregar las fila al panel
    var espacio = document.createElement("br");
    
    // Asigna la clase a los div´s tag creados
    row.className = "row";
    row.setAttribute("id", iterador)
    rowLabelVa.className = "col-md-6 col-md-1";
    rowTextVa.className = "col-md-4 col-md-3";  
    rowLabelCla.className = "col-md-6 col-md-1";
    rowTextCla.className = "col-md-4 col-md-3";

    // Crea los labels y textBox necesarios
    var labelClave = document.createElement('label');
    labelClave.appendChild(document.createTextNode("Clave:"));

    var textClave = document.createElement('input');
    textClave.type = 'text';
    textClave.setAttribute("name", clave + iterador);
    textClave.setAttribute("id", clave + iterador);
    textClave.setAttribute("class", "form-control");

    var labelValor = document.createElement('label');
    labelValor.appendChild(document.createTextNode("Valor:"));

    var textValor = document.createElement('input');
    textValor.type = 'text';
    textValor.setAttribute("name", valor + iterador);
    textValor.setAttribute("id", valor + iterador);
    textValor.setAttribute("class", "form-control"); 

    // añade el elemento creados a los div especificados
    rowLabelCla.appendChild(labelClave);
    rowTextVa.appendChild(textClave);
    rowLabelVa.appendChild(labelValor);
    rowTextCla.appendChild(textValor);

    // Añade los div´s a al div fila
    row.appendChild(rowLabelCla);
    row.appendChild(rowTextVa);
    row.appendChild(rowLabelVa);
    row.appendChild(rowTextCla);

    var currentDiv = document.getElementById("claveValor"); // Obtiene el elemento del DOM donde se va almacenar los div´s
    currentDiv.appendChild(row); // Asigna el div al DOM en la posicion especifica
    currentDiv.appendChild(espacio); // Asigna el br tag a la fila

    iterador = iterador + 1; // aumente la variable iteradora que lleva el conteo del numero de filas creadas
    document.getElementById("iterador").value = String(iterador); // Asigna el valor de la variable a una etiqueta asequible desde codebehind
    return false;
}

// Metodo que oculta el formulario Form-Data y hace visible el formulario de tipo JSON
function Json_Check() {
    document.getElementById("panelClaveValor").style.display = "none";
    document.getElementById("panelJson").style.display = "block";
}

// Metodo que oculta el formulario JSON y hace visible el formulario de tipo Form-Data
function FormData_Check() {
    document.getElementById("panelClaveValor").style.display = "block";
    document.getElementById("panelJson").style.display = "none";
}

function pretty_Check() {
    sinFormato = document.getElementById('resultado').value;
    if (sinFormato.trim() !== "") {
        if (sinFormato.includes("{") && sinFormato.includes("}")) {
            var obj = JSON.parse(sinFormato);
            var formatoJSON = JSON.stringify(obj, undefined, 4);
            document.getElementById('resultado').value = formatoJSON;
            document.getElementById("rdraw").checked = false;
            document.getElementById("rdpty").checked = true;
        }
        else {
            document.getElementById("rdraw").checked = false;
            document.getElementById("rdpty").checked = true;
        }
    }
    else {
        document.getElementById("rdraw").checked = false;
        document.getElementById("rdpty").checked = true;
    }
}

function raw_Check() {
    document.getElementById('resultado').value = sinFormato;
    document.getElementById("rdpty").checked = false;
    document.getElementById("rdraw").checked = true;
}