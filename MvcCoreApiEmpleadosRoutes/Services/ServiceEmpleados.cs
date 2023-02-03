using MvcCoreApiEmpleadosRoutes.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiEmpleadosRoutes.Services
{
    public class ServiceEmpleados
    {

        //FUNCION ASINCRONA: (La palabra clave "async" en C# indica que una función es asíncrona.
        //Las funciones asíncronas permiten la ejecución de tareas de larga duración,
        //como operaciones de red, en segundo plano sin bloquear el hilo de la interfaz de usuario.
        //Esto significa que el hilo principal puede seguir ejecutando tareas mientras la función
        //asíncrona realiza su trabajo en segundo plano)

        //------------------------------------------------------------------------------------------//

        //La variable "UrlApi" es una cadena que representa la URL de una API
        private string UrlApi;

        //La variable "header" es una instancia de la clase "MediaTypeWithQualityHeaderValue"
        //que se utiliza en solicitudes HTTP para especificar el tipo de contenido que se espera
        //en la respuesta. En este caso, se establece en "application/json", lo que indica que se
        //espera una respuesta en formato JSON
        private MediaTypeWithQualityHeaderValue header;


        //El constructor público "ServiceEmpleados" toma un argumento de tipo "string" que representa
        //la URL de la API y lo asigna a la variable "UrlApi". También instancia la variable "header"
        //y le asigna el valor "application/json". Esto significa que cada vez que se cree una nueva
        //instancia de la clase "ServiceEmpleados", se especificará la URL de la API y el tipo de
        //contenido que se espera en la respuesta
        public ServiceEmpleados(string url)
        {
            this.UrlApi= url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //METODO INTERMEDIO PARA LEER LOS DATOS DEL API INDEPENDIENTEMENTE DE LO QUE LEAMOS
        //1) OBJETO QUE DEVUELVE
        //2) PETICION REQUEST

        private async Task<T> GetDatosApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T datos = await response.Content.ReadAsAsync<T>();
                    return datos;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "/api/empleados";
            List<Empleado> empleados = await this.GetDatosApiAsync<List<Empleado>>(request);
            return empleados;
        }

        //CONCLUSION: Con el METODO INTERMEDIO ahorramos mucha implementación de codigo y tiempo

        //EXPLICACION: (Este código define una función asíncrona en C# llamada "GetEmpleadosAsync"
        //que devuelve una lista de objetos "Empleado". Utiliza la clase "HttpClient"
        //para hacer una solicitud HTTP a una URL especificada en la variable "request")
        //public async Task<List<Empleado>> GetEmpleadosAsync()
        //{
        //using (HttpClient client = new HttpClient())
        //{
        //La función comienza creando una nueva instancia de "HttpClient" y estableciendo
        //la dirección base en la URL de la API especificada en "this.UrlApi".
        //También establece los encabezados de la solicitud y agrega un encabezado "Accept"
        //con el valor "application/json".

        //string request = "/api/empleados";
        //client.BaseAddress = new Uri(this.UrlApi);
        //client.DefaultRequestHeaders.Clear();
        //client.DefaultRequestHeaders.Accept.Add(this.header);

        //Luego, hace una solicitud HTTP GET a la dirección "api/empleados". La
        //respuesta a la solicitud se almacena en una variable de tipo
        //"HttpResponseMessage" llamada "response".

        //HttpResponseMessage response = await client.GetAsync(request);
        //if(response.IsSuccessStatusCode)
        //{
        //List<Empleado> empleados = await
        //response.Content.ReadAsAsync<List<Empleado>>();
        //return empleados;
        //}
        //else
        //{
        //return null
        //}

        //Si la respuesta es exitosa, se usa el método "ReadAsAsync" para deserializar
        //el contenido de la respuesta en una lista de cadenas y se devuelve esa lista

        //Anotación:Esta función es asíncrona y se debe llamar utilizando el operador "await" en
        //un contexto asíncrono para obtener el resultado esperado

        //}
        //}

        //public async Task<List<string>> GetOficios()
        // {
        //using (HttpClient client = new HttpClient())
        //{
        //string request = "api/empleados/oficios";
        //client.BaseAddress = new Uri(this.UrlApi);
        //client.DefaultRequestHeaders.Clear();
        //client.DefaultRequestHeaders.Accept.Add(this.header);
        //HttpResponseMessage response =
        //await client.GetAsync(request);
        //if(response.IsSuccessStatusCode)
        //{
        //List<string> oficios = await response.Content.ReadAsAsync<List<string>>();
        //return oficios;
        // }
        //else
        //{
        //return null;
        //}
        //}
        //}

        //EXPLICACION: (Ambos métodos de GetEmpleadosAsync() y GetOficios() son iguales, solo cambia
        //el request y lo que vamos a devolver. Por tanto, podría crear un METODO INTERMEDIO privado
        //devolviera el objeto utilizando las mismas acciones y luego hacer la llamada para ahorrar
        //escritura.
        //Este tipo de métodos se les llama Génericos y se utiliza la letra T para indicar ese tipo.
        //Solo se modifica dos elementos por tanto se recibe en el método estos dos elementos)

        //EL METODO INTERMEDIO LO IMPLEMENTAMOS ANTES DE LOS MÉTODOS, ADEMÁS ELIMINAMOS EL CUERPO DE 
        //CADA METODO


        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "/api/empleados/oficios";
            List<string> oficios = await this.GetDatosApiAsync<List<string>>(request);
            return oficios;
        }

        public async Task<Empleado> FindEmpleadoAsync(int idempleado)
        {
            string request = "/api/empleados/" + idempleado;
            Empleado empleado = await this.GetDatosApiAsync<Empleado>(request);
            return empleado;
        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            string request = "/api/empleados/empleadosoficio/" + oficio;
            List<Empleado> empleados = await this.GetDatosApiAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<Empleado>> GetEmpleadosSalarioAsync(int salario)
        {
            string request = "/api/empleados/empleadossalario/" + salario;
            List<Empleado> empleados = await this.GetDatosApiAsync<List<Empleado>>(request);
            return empleados;
        }








    }
}
