using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;

namespace Z.Server.Repositorio
{
    public class ClienteRepositorio:Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly Context _context;
        public ClienteRepositorio(Context context) : base(context)
        {
            this._context = context;
        }

        public async Task<Cliente> SelectByNombre(string nombre)
        {
            Cliente? clienteDb = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Nombre.Contains(nombre));
            return clienteDb;
        }



    }
}
