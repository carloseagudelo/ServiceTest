<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestClient.aspx.cs" Inherits="ServiceTest.Pages.RestClient" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Cliente servios REST</title>
    <!-- Referencia las librerias de Bootstrap -->
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../js/RestClient.js" type="text/javascript"></script>
</head>

<body>
    <!-- Define un Header en el formulario -->  
    <div class="container">

        <!-- Header del formulario -->
        <div class="jumbotron">
          <h1>Pruebas Servicios Rest FMobileJW</h1>
          <p><a class="btn btn-primary btn-lg" href="#">Documentación</a></p>
        </div> 

        <!-- Define una sesion de formulario -->  
        <form id="formRest" runat="server">

          <!-- Metodos HTTP, Metodos, boton -->
          <div class="row">
            <div class="col-md-6 col-md-1">
              <label for="email">Método:</label>
            </div>
            <!-- Seleccion del metodo HTTP -->
            <div class="col-md-6 col-md-4">
              <select class="form-control" id="metodohttp" runat="server">                
                <option value="GET">GET</option>
                <option value="POST">POST</option>                
                <option value="PUT">PUT</option>
                <option value="PATCH">PATCH</option>
                <option value="DELETE">DELETE</option>
                <option value="TRACE">TRACE</option>
                <option value="OPTIONS">OPTIONS</option>
                <option value="CONNECT">CONNECT</option>
                <option value="PATCH">PATCH</option>
                <option value="SEARCH">SEARCH</option>
              </select>
            </div>            
            <div class="col-md-6 col-md-1">         
              <label for="pwd">Servicio:</label>
            </div>
            <!-- Selección del servicio a probar --> 
            <div class="col-md-6 col-md-5">  
              <input type="text" class="form-control" id="servicios" runat="server">
              </input>  
            </div>
            <!-- Boton que envia la paetición --> 
            <div class="col-md-6 col-md-1">
              <asp:Button class="btn btn-default" 
                          runat="server" 
                          ID="enviar" 
                          Text="ENVIAR" 
                          onclick="enviar_Click" 
                          AutoPostback ="false" />
            </div>
          </div> 
          
          <!-- RadioButton con las opciones del ingreso de parametros -->
          <div class="row">
            <div class="col-md-6 col-md-2">
                <div class="radio">
                  <label><input type="radio" name="optradio" onchange="FormData_Check()" id="rdbfd" runat="server" checked="true">Form-Data</label>
                </div>
            </div>
            <div class="col-md-6 col-md-2">
                <div class="radio">
                  <label><input type="radio" name="optradio" onchange="Json_Check()" id="rdbjs" runat="server" checked="false">JSON</label>
                </div>
            </div>
          </div> 
          
          <!-- Parametros de la petición clave valor --> 
          <div class="panel panel-default" id="panelClaveValor" runat="server">
           <div class="panel-body">
           <div class="row">
               <div class="col-md-1 col-md-1">
                 <label for="pwd">Entradas:</label>
               </div>
             </div>
            <div id="claveValor">
             <div class="row" id="0">
                <div class="col-md-6 col-md-1">
                  <label for="email">Clave:</label>
                </div>
                <div class="col-md-4 col-md-3">
                  <input type="text" id="clave0" class="form-control" runat="server">
                </div>
                <div class="col-md-6 col-md-1">
                  <label for="email">Valor:</label>
                </div>
                <div class="col-md-4 col-md-3">
                  <input type="text" id="valor0" class="form-control" runat="server">
                </div>
                <div class="col-md-4 col-md-3">
                  <asp:Button class="btn btn-default" runat="server" ID="agregar" Text="AGREGAR" 
                        onclick="agregar_Click" OnClientClick="return agregar_Click();" AutoPostback ="true" />
                </div>
             </div>
             <br />
            </div>
           </div>
          </div> 

          <!-- Parametros de la petición FormatoJSON -->
          <div class="panel panel-default" id="panelJson" runat="server">
            <div class="panel-body">
              <div class="row">
                <div class="col-md-1 col-md-1">
                  <label for="pwd">Entradas:</label>
                </div>
              </div>
              <div class="row">
                <div class="col-md-12 col-md-12">
                  <textarea class="form-control" id="parametros" runat="server" rows="5" cols="50" style="resize:none">
                  </textarea>
                </div>
              </div>
           </div>
          </div> 

          <!-- Resultado de la petición -->
          <div class="panel panel-default">
           <div class="panel-body">
             <div class="row">
               <div class="col-md-1 col-md-1">
                 <label for="pwd">Resultado:</label>
               </div>
             </div>
             <div class="row">
               <div class="col-md-6 col-md-2">
                <div class="radio">
                  <label><input type="radio" name="optraw" onchange="raw_Check()" id="rdraw" runat="server" checked="true">Normal</label>
                </div>
               </div>
               <div class="col-md-6 col-md-2">
                <div class="radio">
                  <label><input type="radio" name="optpretty" onchange="pretty_Check()" id="rdpty" runat="server" checked="false">JSON</label>
                </div>
               </div>
             </div>
             <div class="row">
               <div class="col-md-12 col-md-12">
                 <textarea readonly class="form-control" id="resultado" runat="server" rows="8" cols="80" style="resize:vertical" >
                 </textarea>
               </div>
             </div>
           </div>
          </div>

          <!-- Variable de comunicació del codigo JavaScript con C# -->
          <input id="iterador" type="hidden" runat="server" />

        </form>
    </div>
    
</body>

</html>
