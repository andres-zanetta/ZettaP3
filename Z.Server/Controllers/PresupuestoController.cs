using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;
using Z.Server.Repositorio;
using Z.Shared.Enums;

namespace Z.Server.Controllers
{
    [ApiController]
    [Route("api/Presupuesto")]
    public class PresupuestoController : ControllerBase
    {
      
        private readonly IMapper mapper;
        private readonly IPresupuestoRepositorio repositorio;

        public PresupuestoController(IPresupuestoRepositorio repositorio)
        {
           this.repositorio = repositorio;
        }

        // GET: api/Presupuesto
        [HttpGet]
        public async Task<ActionResult<List<Presupuesto>>> Get()
        {
            return await repositorio.SelectByRubro();
        }

        // GET: api/Presupuesto/por-id/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var presupuesto = await repositorio.SelectById(id);

            if (presupuesto == null)
                return NotFound($"El presupuesto con id {id} no existe");

            return presupuesto;
        }


        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            //var existe = await _context.Clientes.AnyAsync(x => x.Id == id);
            //return existe;
            return await repositorio.Existe(id);
        }

        // GET: api/Presupuesto/por-cliente/3
        [HttpGet("por-cliente/{clienteId:int}")]
        public async Task<ActionResult<List<Presupuesto>>> GetByCliente(int clienteId)
        {
            // Reemplaza la línea problemática en el método GetByCliente
            var presupuestos = await repositorio.SelectByCliente(clienteId);
            // Solución: filtra por ClienteId en vez de Cliente
            var filtrados = presupuestos
                .OfType<Presupuesto>()
                .Where(p => p.ClienteId == clienteId)
                .ToList();

            if (!filtrados.Any())
                return NotFound($"No se encontraron presupuestos para el cliente {clienteId}");

            return filtrados;
        }

        // GET: api/Presupuesto/por-rubro/Electricidad
        [HttpGet("por-rubro/{rubro}")]
        public async Task<ActionResult<List<Presupuesto>>> GetByRubro(Rubro rubro)
        {
            var presupuestos = await repositorio.SelectByRubro();
            var filtrados = presupuestos.Where(p => p.Rubro == rubro).ToList();

            if (!filtrados.Any())
                return NotFound($"No se encontraron presupuestos para el rubro {rubro}");

            return filtrados;
        }

        // POST: api/Presupuesto
        [HttpPost]
        public async Task<ActionResult<int>> Post(Presupuesto p)
        {
            try
            {
              return await repositorio.Insert(p);
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
            try
            {
                if (id != p.Id)
                {
                    return BadRequest("El id no coincide con el presupuesto enviado");
                }
                var presupuestoDb = await repositorio.Update(id, p);

                if (!presupuestoDb)
                {
                    return BadRequest($"El presupuesto con id {id} no existe");
                }
                return Ok();

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
            var r = await repositorio.Existe(id);
            if (!r)
            {
                return NotFound($"El presupuesto con id {id} no existe");
                
            }
            return Ok();


        }
    }
}
