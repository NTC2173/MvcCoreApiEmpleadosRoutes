using Microsoft.AspNetCore.Mvc;
using MvcCoreApiEmpleadosRoutes.Models;
using MvcCoreApiEmpleadosRoutes.Services;

namespace MvcCoreApiEmpleadosRoutes.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadosController (ServiceEmpleados service)
        {
            this.service = service;
        }

        public async Task<IActionResult> EmpleadosOficio()
        {
            List<Empleado> empleados = await this.service.GetEmpleadosAsync();
            List<string> oficios = await this.service.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;

            //ViewData["OFICIOS"] = oficios --> El objeto "ViewData" es una propiedad de un
            //controlador que se utiliza para almacenar información que se mostrará en una vista.
            //La entrada "OFICIOS" se puede acceder en una vista y utilizar para generar
            //contenido HTML

            return View(empleados);
        }

        //[HttpPost] --> Indica que el método se ejecutará en respuesta a una solicitud HTTP POST.
        //Las solicitudes HTTP POST se utilizan para enviar datos desde un cliente a un servidor.
        //Cuando un formulario web se envía mediante una solicitud HTTP POST, los datos en el
        //formulario se incluyen en el cuerpo de la solicitud y se pueden acceder en el
        //controlador mediante la anotación [HttpPost]


        [HttpPost]
        public async Task<IActionResult> EmpleadosOficio(string oficio)
        {
            List<Empleado> empleados = await this.service.GetEmpleadosOficioAsync(oficio);
            List<string> oficios = await this.service.GetOficiosAsync();
            ViewData["OFICIOS"] = oficios;
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            Empleado empleado = await this.service.FindEmpleadoAsync(id);
            return View(empleado);
        }
    }
}
