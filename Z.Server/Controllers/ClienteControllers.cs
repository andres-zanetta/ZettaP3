using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;
using Z.Shared.DTOS;

namespace Z.Server.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClienteControllers : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper maper;

        public ClienteControllers(Context context)
        {
            this._context = context;
            this.maper = maper;

        }
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            Cliente? C = await _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (C == null)
            {
                return NotFound($"El cliente con id {id} no existe");
            }
            return C;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<Cliente>>> Get(string nombre)
        {
            var clientes = await _context.Clientes.Where(c => c.Nombre.Contains(nombre)).ToListAsync();

            if (!clientes.Any())
                return NotFound($"No se encontraron clientes con el nombre que contiene '{nombre}'");

            return clientes;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            //var existe = await _context.Clientes.AnyAsync(x => x.Id == id);
            //return existe;
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(CrearClienteDTO cdto)
        {
            try
            {
                //Cliente c = new Cliente();
                //c.Nombre = cdto.Nombre;
                //c.Direccion = cdto.Direccion;
                //c.Telefono = cdto.Telefono;
                //c.Email = cdto.Email;

                Cliente c=maper.Map<Cliente>(cdto);

                _context.Clientes.Add(c);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
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
            var clienteDb = await _context.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();

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
                _context.Clientes.Update(clienteDb);
                await _context.SaveChangesAsync();
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
            var existe = await _context.Clientes.AnyAsync(y => y.Id == id);
            if (!existe)
            {
                return NotFound($"El cliente con id {id} no existe");
            }
           Cliente EntidadBorrar = new Cliente();
            EntidadBorrar.Id = id;

            _context.Remove(EntidadBorrar);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
