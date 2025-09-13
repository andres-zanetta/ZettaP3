using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;

namespace Z.Server.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClienteControllers: ControllerBase
    {
        private readonly Context _context;

        public ClienteControllers(Context context)
        {
            this._context = context;

        }
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>>Get()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Cliente c)
        {
            try
            {
                _context.Clientes.Add(c);
                await _context.SaveChangesAsync();
                return c.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody]Cliente c)
        {
            if (id != c.Id)
            {
                return BadRequest("El id del cliente no coincide con el id de la url");
            }
            var clienteDb = await _context.Clientes.Where(c=>c.Id==id).FirstOrDefaultAsync();

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
                return BadRequest(e.Message);
            }

        }
    }
}
