using AutoMapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Domain.Models;

namespace Bitzen.Application.Mapper
{
    public class CombustivelMapper<T> where T : class
    {
        public static T Map(object source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Combustivel, AdicionarCombustivelQuery>().ReverseMap();
                cfg.CreateMap<Combustivel, AtualizarCombustivelQuery>().ReverseMap();
                cfg.CreateMap<Combustivel, CombustivelResponse>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<T>(source);
        }
    }
}
