using AutoMapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Responses;
using Bitzen.Domain.Models;

namespace Bitzen.Application.Mapper
{
    public class AbastecimentoMapper<T> where T : class
    {
        public static T Map(object source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Abastecimento, AdicionarAbastecimentoQuery>().ReverseMap();
                cfg.CreateMap<Abastecimento, AbastecimentoResponse>().ReverseMap();
                cfg.CreateMap<Motorista, MotoristaResponse>().ReverseMap();
                cfg.CreateMap<Veiculo, VeiculoResponse>().ReverseMap();
                cfg.CreateMap<Combustivel, CombustivelResponse>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();

            return iMapper.Map<T>(source);
        }
    }
}
