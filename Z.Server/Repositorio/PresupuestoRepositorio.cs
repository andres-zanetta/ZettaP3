using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;
using Z.Shared.Enums;

namespace Z.Server.Repositorio
{
    public class PresupuestoRepositorio:Repositorio<Presupuesto>, IPresupuestoRepositorio
    {
        private readonly Context _context;  
        public PresupuestoRepositorio(Context context) : base(context)
        {
            this._context = context;
        }

        public async Task<List<Presupuesto>> SelectByRubro(Rubro rubro)
        {
            var presupuestos = await _context.Presupuestos
                .AsNoTracking()
                .Where(p => p.Rubro == rubro)
                .ToListAsync();
            return presupuestos;
        }

        public async Task<IEnumerable<object>> SelectByCliente(int id)
        {
            // Devuelve la lista de presupuestos filtrados por ClienteId como IEnumerable<object>
            return await _context.Presupuestos
                .AsNoTracking()
                .Where(p => p.ClienteId == id)
                .ToListAsync();
        }
    }
}
