using Z.BD.DATA;

namespace Z.Server.Repositorio
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<int> Insert(E entidad);
        Task<List<E>> SelectByRubro();
        Task<E> SelectById(int id);
        Task<bool> Update(int id, E entidad);
    }
}