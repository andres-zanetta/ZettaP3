using Z.BD.DATA.Entity;
using Z.Shared.Enums;
using System.Collections.Generic;

namespace Z.Server.Repositorio
{
    public interface IPresupuestoRepositorio : IRepositorio<Presupuesto>
    {
        Task<IEnumerable<object>> SelectByCliente(int id);
        Task<List<Presupuesto>> SelectByRubro(Rubro rubro);
    }
}
