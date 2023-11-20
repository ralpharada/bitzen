using AutoMapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Domain.Models;

namespace Bitzen.Application.Mapper
{
    public class VeiculoMapper<T> where T : class
    {
        public static T Map(object source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Veiculo, AdicionarVeiculoQuery>().ReverseMap();
                cfg.CreateMap<Veiculo, AtualizarVeiculoQuery>().ReverseMap();
                cfg.CreateMap<Veiculo, VeiculoResponse>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<T>(source);
        }
    }
}
