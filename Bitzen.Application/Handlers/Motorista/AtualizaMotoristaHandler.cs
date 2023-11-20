using Bitzen.Application.Core;
using Bitzen.Application.Queries;
using Bitzen.Application.Util;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;

namespace Bitzen.Application.Handlers
{
    public class AtualizarMotoristaHandler : IRequestHandler<AtualizarMotoristaQuery, IEvent>
    {
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        public AtualizarMotoristaHandler(IMotoristaRepository motoristaRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _motoristaRepository = motoristaRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(AtualizarMotoristaQuery request, CancellationToken cancellationToken)
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

                if (String.IsNullOrWhiteSpace(request.Nome))
                    return new ResultEvent(success, "O campo Nome é obrigatório.");

                if (request.Nome.Length > 255)
                    return new ResultEvent(success, "O campo Nome não pode conter acima de 255 caracteres.");

                if (String.IsNullOrWhiteSpace(request.CPF))
                    return new ResultEvent(success, "O campo CPF é obrigatório.");

                if (request.CPF.Length > 14 || !ValidaCPF.Validar(request.CPF))
                    return new ResultEvent(success, "O CPF informado é inválido.");

                if (String.IsNullOrWhiteSpace(request.NumeroCNH))
                    return new ResultEvent(success, "O campo Número CNH é obrigatório.");

                if (request.NumeroCNH.Length > 11)
                    return new ResultEvent(success, "O campo Número CNH é inválido.");

                if (String.IsNullOrWhiteSpace(request.CategoriaCNH))
                    return new ResultEvent(success, "O campo Categoria CNH é obrigatório.");

                if (!ValidaData.Validar(request.DataNascimento))
                    return new ResultEvent(success, "O campo Data Nascimento deve ser preenchido corretamente.");

                #endregion

                var motorista = await _motoristaRepository.ObterPorId(request.Id, cancellationToken);
                motorista.Nome = request.Nome;
                motorista.CPF = request.CPF;
                motorista.NumeroCNH = request.NumeroCNH;
                motorista.CategoriaCNH = request.CategoriaCNH;
                motorista.DataNascimento = request.DataNascimento;
                motorista.Status = request.Status;

                var result = await _motoristaRepository.Salvar(motorista, cancellationToken);
                success = result > 0;

                return new ResultEvent(success, success ? "Motorista atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }
        }

    }
}
