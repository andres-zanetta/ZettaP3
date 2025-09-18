using AutoMapper;
using Z.BD.DATA.Entity;
using Z.Shared.DTOS;

namespace Z.Server.Util
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            // CrearClienteDTO -> Cliente
            CreateMap<CrearClienteDTO,Cliente>();

        }
    }
}
