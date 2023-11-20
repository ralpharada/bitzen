using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Application.Util;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AdicionarAbastecimentoHandler : IRequestHandler<AdicionarAbastecimentoQuery, IEvent>
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICombustivelRepository _combustivelRepository;

        public AdicionarAbastecimentoHandler(IAbastecimentoRepository abastecimentoRepository,
            IVeiculoRepository veiculoRepository,
            IMotoristaRepository motoristaRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            ICombustivelRepository combustivelRepository)
        {
            _abastecimentoRepository = abastecimentoRepository;
            _veiculoRepository = veiculoRepository;
            _motoristaRepository = motoristaRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _combustivelRepository = combustivelRepository;
        }

        public async Task<IEvent> Handle(AdicionarAbastecimentoQuery request, CancellationToken cancellationToken)
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
                if (request.VeiculoId <= 0)
                    return new ResultEvent(success, "O campo Veículo é obrigatório.");

                if (request.MotoristaResponsavelId <= 0)
                    return new ResultEvent(success, "O campo Motorista é obrigatório.");

                var veiculo = await _veiculoRepository.ObterPorId(request.VeiculoId, cancellationToken);
                if (veiculo == null)
                    return new ResultEvent(success, "Veículo não localizado.");

                if (String.IsNullOrWhiteSpace(request.Data))
                    return new ResultEvent(success, "O campo Data é obrigatório.");

                if (!ValidaData.Validar(request.Data))
                    return new ResultEvent(success, "O campo Data é inválido.");

                if (request.QuantidadeAbastecida > veiculo.CapacidadeMaximaTanque)
                    return new ResultEvent(success, $"A Quantidade Abastecida deve ser no máximo {veiculo.CapacidadeMaximaTanque}.");

                if (request.CombustivelId != veiculo.CombustivelId)
                    return new ResultEvent(success, $"O Tipo de Combustível deve ser {veiculo.Combustivel.Descricao}.");

                var motorista = await _motoristaRepository.ObterPorId(request.MotoristaResponsavelId, cancellationToken);
                if (motorista == null)
                    return new ResultEvent(success, "Motorista não localizado.");

                #endregion

                var combustivel = await _combustivelRepository.ObterPorId(request.CombustivelId, cancellationToken);

                var abastecimento = AbastecimentoMapper<Abastecimento>.Map(request);
                abastecimento.CombustivelPreco = combustivel.Preco;
                abastecimento.TotalAbastecimento = abastecimento.QuantidadeAbastecida * combustivel.Preco;

                var upsertedId = await _abastecimentoRepository.Salvar(abastecimento, cancellationToken);
                success = upsertedId > 0;

                return new ResultEvent(success, success ? "Abastecimento cadastrado com sucesso!" : "Falha ao cadastrar o Abastecimento!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
