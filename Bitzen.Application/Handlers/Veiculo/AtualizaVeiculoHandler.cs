using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AtualizarVeiculoHandler : IRequestHandler<AtualizarVeiculoQuery, IEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICombustivelRepository _combustivelRepository;
        public AtualizarVeiculoHandler(IVeiculoRepository veiculoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            ICombustivelRepository combustivelRepository)
        {
            _veiculoRepository = veiculoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _combustivelRepository = combustivelRepository;
        }

        public async Task<IEvent> Handle(AtualizarVeiculoQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(success, "Acesso expirado");

                #endregion

                #region Validação dos campos

                if (request.Placa.Length != 7)
                    return new ResultEvent(success, "O campo Placa do Veículo é inválido.");

                if (String.IsNullOrWhiteSpace(request.NomeVeiculo))
                    return new ResultEvent(success, "O campo Nome do Veículo é obrigatório.");

                if (request.NomeVeiculo.Length > 255)
                    return new ResultEvent(success, "O campo Nome do Veículo não pode conter acima de 255 caracteres.");

                var combustivel = await _combustivelRepository.ObterPorId(request.CombustivelId, cancellationToken);

                if (combustivel == null)
                    return new ResultEvent(success, "O Tipo Combustível é inválido.");

                if (String.IsNullOrWhiteSpace(request.Fabricante))
                    return new ResultEvent(success, "O campo Fabricante é obrigatório.");

                if (request.Fabricante.Length > 255)
                    return new ResultEvent(success, "O campo Fabricante não pode conter acima de 255 caracteres.");

                if (request.AnoFabricacao < 1)
                    return new ResultEvent(success, "O campo Ano de Fabricação é obrigatório.");

                if (request.CapacidadeMaximaTanque <= 0)
                    return new ResultEvent(success, "O campo Capacidade Máxida do Tanque é obrigatório.");

                #endregion

                var veiculo = await _veiculoRepository.ObterPorId(request.Id, cancellationToken);
                veiculo.Placa = request.Placa;
                veiculo.NomeVeiculo = request.NomeVeiculo;
                veiculo.CombustivelId = request.CombustivelId;
                veiculo.Fabricante = request.Fabricante;
                veiculo.AnoFabricacao = request.AnoFabricacao;
                veiculo.CapacidadeMaximaTanque = request.CapacidadeMaximaTanque;
                veiculo.Observacoes = request.Observacoes;

                var result = await _veiculoRepository.Salvar(veiculo, cancellationToken);
                success = result > 0;

                return new ResultEvent(success, success ? "Veiculo atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }
        }

    }
}
