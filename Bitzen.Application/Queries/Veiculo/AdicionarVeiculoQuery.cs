using Bitzen.Application.Core;
using Bitzen.Core.Events;

namespace Bitzen.Application.Queries
{
    public class AdicionarVeiculoQuery : Request<IEvent>
    {
        public string Placa { get; private set; }
        public string NomeVeiculo { get; private set; } 
        public int CombustivelId { get; private set; }
        public string Fabricante { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int CapacidadeMaximaTanque { get; set; }
        public string Observacoes { get; set; }
        public AdicionarVeiculoQuery(string placa, string nomeVeiculo, int combustivelId, string fabricante, int anoFabricacao, int capacidadeMaximaTanque,string observacoes)
        {
            Placa = placa.Trim();
            NomeVeiculo = nomeVeiculo.Trim();
            CombustivelId = combustivelId;
            Fabricante = fabricante.Trim();
            AnoFabricacao = anoFabricacao;
            CapacidadeMaximaTanque = capacidadeMaximaTanque;
            Observacoes = observacoes.Trim();
        }
    }
}
