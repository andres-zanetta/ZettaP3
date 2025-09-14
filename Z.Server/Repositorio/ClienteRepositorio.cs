using Z.BD.DATA;
using Z.BD.DATA.Entity;

namespace Z.Server.Repositorio
{
    public class ClienteRepositorio:Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(Context context) : base(context)
        {
        }



    }
}
