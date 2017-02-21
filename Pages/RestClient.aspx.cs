/**
 * 
 * @author: Carlos Enrique Agudelo Gira
 * @email: c.agudelo.giraldo@accenture.com, agudelo.carlos@hotmail.es
 * @description: Codebehind for interface of client of the form for testing the HTTP methods of the FMOBILE application
 * @date crreate 31/01/2017
 * @date crreate 09/02/2017
 *
 */
namespace ServiceTest.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Net;
    using System.Text;
    using System.IO;
    using System.Reflection;
    using System.Web.UI.HtmlControls;
    
    public partial class RestClient : System.Web.UI.Page
    {
        /// <summary>
        /// Evento que carga la informacion inicial y eventos iniciales al formulario.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            panelJson.Attributes.Add("style", "display:none"); // Agrega una propiedad de estilo para el tag           
            ReestablecerValores();
        }

        /// <summary>
        /// Evento del boton Enviar del formuario.
        /// </summary>
        protected void enviar_Click(object sender, EventArgs e)
        {
            if (rdbfd.Checked) // Valida si la opción de parametros es la de form-data
            {
                string Parametros = string.Format("{0}", "{"); // Crea e inicializa la variable con los parametros
                if (!iterador.Value.ToString().Equals(string.Format("")))
                {
                    int numero = Convert.ToInt32(iterador.Value);
                    for (int i = 0; i < numero; i++) // Itera sobre el número de parametros
                    {
                        if (!Request.Form["clave" + i].ToString().Trim().Equals(string.Format("")) && !Request.Form["valor" + i].ToString().Trim().Equals(string.Format(""))) // Valida que los campos de calve y valor para la fila iterada no esten nulos
                        {
                            Parametros = Parametros + string.Format("\"{0}\": \"{1}\", ", Request.Form["clave" + i].ToString().Trim(), Request.Form["valor" + i].ToString().Trim()); // Captura la información
                        }                    
                    }
                    Parametros = Parametros.Trim().Substring(0, Parametros.Trim().Length - 1) + "}";
                    string respuesta = CapturarRespuestaMetodo(Request.Form["metodohttp"].ToString().Trim(), Request.Form["servicios"].ToString().Trim(), Parametros.Trim()); // Invoca el metodo que realiza la peticion HTTP al servidor
                    resultado.InnerText = respuesta; // Asigna la respuesta al TextArea que muestra la respuesta al usuario
                }
            }
            else
            {
                string respuesta = CapturarRespuestaMetodo(Request.Form["metodohttp"].ToString().Trim(), Request.Form["servicios"].ToString().Trim(), Request.Form["parametros"].ToString().Trim()); // Invoca el metodo que realiza la peticion HTTP al servidor
                resultado.InnerText = respuesta; // Asigna la respuesta al TextArea que muestra la respuesta al usuario  
            }
            iterador.Value = string.Format("1");
            ReestablecerValores();            
        }

        /// <summary>
        /// Metodo que realiza la petición HTTP a lo recursos disponibles.
        /// </summary>
        /// <param name="metodo">Nombre del metodo HTTP a probar</param>
        /// <param name="servicio">Nombre del servicio a probar</param>
        /// <param name="parametros">Parametros que recibe la petición HTTP</param>
        /// <returns>En caso de parametrizarse bien la petición seria la respuesta dada por el servidor al servicio parametrizado, de lo contrario mensaje indicando que la petición no se parametrizó correctamente</returns>
        private string CapturarRespuestaMetodo(string metodo, string servicio, string parametros)
        {
            string resultado = string.Format(""); // Instania un variable donde se almaenara el mensaje que retorna el servidor

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format(servicio)); //(HttpWebRequest)WebRequest.Create(string.Format("http://{1}//FMobileJW_Prod/Mobile.svc/json/{0}", servicio, HttpContext.Current.Request.Url.Host)); // Crea una intancia de objeto de tipo httpWebRequest asignando la url del servicio, donde el host de la url se captura directamente de la url donde se ejecuta el formulario
            //httpWebRequest.ContentType = "application/json"; // Asigna el ContentType a la paraetrización del servicio
            httpWebRequest.Method = metodo; // Asigna el metodo a la parametrización del servicio

            HttpWebResponse httpResponse = null;

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) // Inicializa un flujo de salida con la información parametrizada del servicio
                {
                    string json = parametros; // Obtiene los parametros enviados por el usuario para la petición
                    streamWriter.Write(json); // Envia la información de los paratros en el flujo de salida
                    streamWriter.Flush(); // Limpia el buffer de salida anteriormente usado
                    streamWriter.Close(); // Cierra el buffer de salida
                }

                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); // Envia la petición al servidor, y obtiene la respuesta del servicio
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) // inicializa un flujo de entrada con el resultado de la respuesta del servidor
                {
                    resultado = streamReader.ReadToEnd().ToString(); // Obtiene la respuesta del servicio y la asigna a la variable retorno                   
                }
            }
            catch (WebException wex)
            {
                if (httpResponse != null)
                {
                    resultado = string.Format("{0} {1}", wex.Message, httpResponse.GetResponseStream().ToString()); // Captura el error generado por el servidor y lo mustra al usuario
                }
                else
                {
                    resultado = string.Format("{0}", wex.Message); // Captura el error generado por el servidor y lo mustra al usuario
                }
            }
            catch(ProtocolViolationException exp)
            {
                if (httpResponse != null)
                {
                    resultado = string.Format("{0} {1}", exp.Message, httpResponse.GetResponseStream().ToString()); // Captura el error generado por el servidor y lo mustra al usuario
                }
                else
                {
                    resultado = string.Format("{0}", exp.Message); // Captura el error generado por el servidor y lo mustra al usuario
                }
            }
            catch (Exception ex)
            {
                if (httpResponse != null)
                {
                    resultado = string.Format("{0} {1}", ex.Message, httpResponse.GetResponseStream().ToString()); // Captura el error generado por el servidor y lo mustra al usuario
                }
                else
                {
                    resultado = string.Format("{0}", ex.Message); // Captura el error generado por el servidor y lo mustra al usuario
                }
            }
            
            return resultado; // Retorna el mensaje del servidor
        }

        /// <summary>
        /// Evento del boton agregar, el cual agrega campos adicionales para agregar parametros a la petición
        /// </summary>
        protected void agregar_Click(object sender, EventArgs e)
        {            
        }

        /// <summary>
        /// Metodo que restablece el valor de los componentes a su valor inicial
        /// </summary>
        private void ReestablecerValores()
        {
            if (rdbfd.Checked)
            {
                metodohttp.SelectedIndex = 0; // Reestablede el valor del select tag metodos HTTP
                clave0.Value = ""; // Resstablece e valor del textbox clave inicial
                valor0.Value = ""; // Reestablece el valor del textbox valor inicial
                parametros.Value = ""; // Reestablece el valor del textArea parametros
                rdbfd.Checked = true; // Reestablece el valor inicial para los radio Buttons
                rdbjs.Checked = false;
                rdraw.Checked = true;
                rdpty.Checked = false;
                panelJson.Attributes.Add("style", "display:none"); // Oculta el elemento en el formulario
                panelClaveValor.Attributes.Add("style", "display:block"); // Muestra el elemento en el formulario 
            }
            else
            {
                metodohttp.SelectedIndex = 0; // Reestablede el valor del select tag metodos HTTP
                clave0.Value = ""; // Resstablece e valor del textbox clave inicial
                valor0.Value = ""; // Reestablece el valor del textbox valor inicial
                parametros.Value = ""; // Reestablece el valor del textArea parametros
                rdbfd.Checked = false; // Reestablece el valor inicial para los radio Buttons
                rdbjs.Checked = true;
                rdraw.Checked = true;
                rdpty.Checked = false;
                panelClaveValor.Attributes.Add("style", "display:none"); // Oculta el elemento en el formulario
                panelJson.Attributes.Add("style", "display:block"); // Muestra el elemento en el formulario 
            }
            
        }

        /// <summary>
        /// Evento del cambio de valor en el select tag.
        /// </summary>
        protected void servicios_SelectedIndexChanged1(object sender, EventArgs e)
        {
                      
        }
    }
}