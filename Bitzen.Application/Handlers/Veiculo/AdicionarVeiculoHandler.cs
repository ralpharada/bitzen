using Bitzen.Application.Core;
using Bitzen.Application.Mapper;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using Bitzen.Domain.Models;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AdicionarVeiculoHandler : IRequestHandler<AdicionarVeiculoQuery, IEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICombustivelRepository _combustivelRepository;

        public AdicionarVeiculoHandler(IVeiculoRepository veiculoRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            ICombustivelRepository combustivelRepository)
        {
            _veiculoRepository = veiculoRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _combustivelRepository = combustivelRepository;
        }

        public async Task<IEvent> Handle(AdicionarVeiculoQuery request, CancellationToken cancellationToken)
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


                if (String.IsNullOrWhiteSpace(request.Placa))
                    return new ResultEvent(success, "O campo Placa do Veículo é obrigatório.");

                if (request.Placa.Length != 7)
                    return new ResultEvent(success, "O campo Placa do Veículo é inválido.");

                var verificacao = await _veiculoRepository.VerificaSeExistePorPlaca(request.Placa, cancellationToken);
                if (verificacao)
                    return new ResultEvent(success, "Placa do Veículo já cadastrado.");

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

                var veiculo = VeiculoMapper<Veiculo>.Map(request);
                veiculo.DataCadastro = DateTime.Now;
                var upsertedId = await _veiculoRepository.Salvar(veiculo, cancellationToken);
                success = upsertedId > 0;

                return new ResultEvent(success, success ? "Veiculo cadastrado com sucesso!" : "Falha ao cadastrar o Veiculo!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
