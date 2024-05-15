using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIReactCrud.Models;

namespace WebAPIReactCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly ReactDbcrudContext _context;
        public EmpleadoController(ReactDbcrudContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _context.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e=>e.IdEmpleado == id);
            return StatusCode(StatusCodes.Status200OK, empleado);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Empleado model)
        {
            await _context.Empleados.AddAsync(model);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new {mensaje = "OK"}); 
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Empleado model)
        {
            _context.Empleados.Update(model);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK" });
        }
    }
}