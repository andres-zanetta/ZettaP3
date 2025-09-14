using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.BD.DATA;
using Z.BD.DATA.Entity;
using Z.Shared.DTOS;

namespace Z.Server.Repositorio
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    {
        private readonly Context context;
        public Repositorio(Context context)
        {
            this.context = context;
        }
        public async Task<bool> Existe(int id)
        {
            var existe = await context.Set<E>().AnyAsync(x => x.Id == id);
            return existe;

        }
        public async Task<E> SelectById(int id)
        {
            E? clienteDb = await context.Set<E>().AsNoTracking().FirstOrDefaultAsync(ret => ret.Id == id);
            return clienteDb;
        }

        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {
                //Cliente c = new Cliente();
                //c.Nombre = cdto.Nombre;
                //c.Direccion = cdto.Direccion;
                //c.Telefono = cdto.Telefono;
                //c.Email = cdto.Email;
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            var clienteDb = await SelectById(id);

            if (clienteDb == null)
            {
                return false;
            }

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> Delete(int id)
        {
            var clienteDb = await SelectById(id);
            if (clienteDb == null)
            {
                return false;
            }
            context.Set<E>().Remove(clienteDb);
            await context.SaveChangesAsync();
            return true;




        }
    }
}

//Diccionario 
// E= hace generica la clase, para todas aquellas entidades que definimos, esta "E" responde la interfaz IEntityBase 

//AsNoTracking() par que no queden datos en memoria

