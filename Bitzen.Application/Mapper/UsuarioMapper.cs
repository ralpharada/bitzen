using AutoMapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Domain.Models;

namespace Bitzen.Application.Mapper
{
    public class UsuarioMapper<T> where T : class
    {
        public static T Map(object source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, AdicionarUsuarioQuery>().ReverseMap();
                cfg.CreateMap<Usuario, AtualizarUsuarioQuery>().ReverseMap();
                cfg.CreateMap<Usuario, UsuarioResponse>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<T>(source);
        }
    }
}
