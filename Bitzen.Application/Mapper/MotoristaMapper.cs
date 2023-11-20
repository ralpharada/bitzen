using AutoMapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Domain.Models;

namespace Bitzen.Application.Mapper
{
    public class MotoristaMapper<T> where T : class
    {
        public static T Map(object source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Motorista, AdicionarMotoristaQuery>().ReverseMap();
                cfg.CreateMap<Motorista, AtualizarMotoristaQuery>().ReverseMap();
                cfg.CreateMap<Motorista, MotoristaResponse>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<T>(source);
        }
    }
}
