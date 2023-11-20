using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AdicionarAbastecimentoQuery : Request<IEvent>
    {
        public int VeiculoId { get; private set; }
        public int MotoristaResponsavelId { get; private set; }
        public int CombustivelId { get; private set; }
        public int QuantidadeAbastecida { get; private set; }
        public string Data { get; private set; }
        public AdicionarAbastecimentoQuery(int veiculoId, int motoristaResponsavelId, int combustivelId, int quantidadeAbastecida, string data)
        {
            VeiculoId = veiculoId;
            MotoristaResponsavelId = motoristaResponsavelId;
            CombustivelId = combustivelId;
            QuantidadeAbastecida = quantidadeAbastecida;
            Data = data;
        }
    }
}
