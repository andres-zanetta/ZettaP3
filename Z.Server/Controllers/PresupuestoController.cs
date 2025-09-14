using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;
using Z.Shared.Enums;

namespace Z.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuesto")]
    public class PresupuestoController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper mapper;

        public PresupuestoController(Context context, IMapper mapper)
        {
            _context = context;
        }

        // GET: api/Presupuesto
        [HttpGet]
        public async Task<ActionResult<List<Presupuesto>>> Get()
        {
            return await _context.Presupuestos
                .Include(p => p.Cliente)
                .Include(p => p.Items)
                .ToListAsync();
        }

        // GET: api/Presupuesto/por-id/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var presupuesto = await _context.Presupuestos
                .Include(p => p.Cliente)
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (presupuesto == null)
                return NotFound($"El presupuesto con id {id} no existe");

            return presupuesto;
        }


        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            //var existe = await _context.Clientes.AnyAsync(x => x.Id == id);
            //return existe;
            return await _context.Presupuestos.AnyAsync(c => c.Id == id);
        }

        // GET: api/Presupuesto/por-cliente/3
        [HttpGet("por-cliente/{clienteId:int}")]
        public async Task<ActionResult<List<Presupuesto>>> GetByCliente(int clienteId)
        {
            var presupuestos = await _context.Presupuestos
                .Where(p => p.ClienteId == clienteId)
                .Include(p => p.Items)
                .ToListAsync();

            if (!presupuestos.Any())
                return NotFound($"No se encontraron presupuestos para el cliente {clienteId}");

            return presupuestos;
        }

        // GET: api/Presupuesto/por-rubro/Electricidad
        [HttpGet("por-rubro/{rubro}")]
        public async Task<ActionResult<List<Presupuesto>>> GetByRubro(Rubro rubro)
        {
            var presupuestos = await _context.Presupuestos
                .Where(p => p.Rubro == rubro)
                .Include(p => p.Cliente)
                .Include(p => p.Items)
                .ToListAsync();

            if (!presupuestos.Any())
                return NotFound($"No se encontraron presupuestos para el rubro {rubro}");

            return presupuestos;
        }

        // POST: api/Presupuesto
        [HttpPost]
        public async Task<ActionResult<Presupuesto>> Post(Presupuesto p)
        {
            try
            {
                _context.Presupuestos.Add(p);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
            }
            catch
            {
                return BadRequest("Ocurrió un error al crear el presupuesto");
            }
        }

        // PUT: api/Presupuesto/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Presupuesto p)
        {
            if (id != p.Id)
                return BadRequest("El id no coincide con el presupuesto enviado");

            var presupuestoDb = await _context.Presupuestos.FindAsync(id);

            if (presupuestoDb == null)
                return NotFound($"El presupuesto con id {id} no existe");

            presupuestoDb.ClienteId = p.ClienteId;
            presupuestoDb.Rubro = p.Rubro;
            presupuestoDb.Total = p.Total;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(presupuestoDb);
            }
            catch
            {
                return BadRequest("Ocurrió un error al actualizar el presupuesto");
            }
        }

        // DELETE: api/Presupuesto/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
                return NotFound($"El presupuesto con id {id} no existe");

            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
