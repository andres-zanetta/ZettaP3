using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;
using Z.Server.Repositorio;
using Z.Shared.DTOS;

namespace Z.Server.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClienteControllers : ControllerBase
    {
      
        private readonly IMapper maper;
        private readonly IClienteRepositorio repositorio;

        public ClienteControllers(IClienteRepositorio repositorio, IMapper maper)
        {
         
            this.maper = maper;
            this.repositorio = repositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            
            Cliente? C = await repositorio.SelectById(id);
            if (C == null)
            {
                return NotFound($"El cliente con id {id} no existe");
            }
            return C;
        }

        [HttpGet("GetByNombre/{nombre}")]
        public async Task<ActionResult<List<Cliente>>> GetByNombre(string nombre)
        {
            Cliente? C = await repositorio.SelectByNombre(nombre);

            if (C == null)
            {
                return NotFound($"No se encontraron clientes con el nombre que contiene '{nombre}'");
            }
            // El método debe devolver una lista, pero actualmente devuelve un solo Cliente.
            // Solución: Cambiar el tipo de retorno y la lógica para devolver una lista.
            // Si SelectByNombre realmente devuelve una lista, cambia la firma del repositorio y aquí.
            // Si no, crea una lista con el resultado.

            return new List<Cliente> { C };
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
           
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearClienteDTO cdto)
        {
            try
            {
                //Cliente c = new Cliente();
                //c.Nombre = cdto.Nombre;
                //c.Direccion = cdto.Direccion;
                //c.Telefono = cdto.Telefono;
                //c.Email = cdto.Email;

                Cliente c=maper.Map<Cliente>(cdto);

          

                return await repositorio.Insert(c);
            }
            catch
            {
                return BadRequest("Ocurrió un error al guardar el cliente");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Cliente c)
        {
            if (id != c.Id)
            {
                return BadRequest("El id del cliente no coincide con el id de la url");
            }
            var clienteDb = await repositorio.SelectById(id);

            if (clienteDb == null)
            {
                return NotFound($"El cliente con id {id} no existe");
            }

            clienteDb.Nombre = c.Nombre;
            clienteDb.Direccion = c.Direccion;
            clienteDb.Telefono = c.Telefono;
            clienteDb.Email = c.Email;

            try
            {
                await repositorio.Update(id, clienteDb);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Ocurrió un error al actualizar el cliente");
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El cliente con id {id} no existe");
            }
            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Ocurrió un error al eliminar el cliente");
            }
            
        }
    }
}
