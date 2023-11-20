using Bitzen.Application.Contracts;
using Bitzen.Application.Crypto;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bitzen.Application.Handlers.Autenticacao
{
    public class AutenticacaoUsuarioHandler : IRequestHandler<AutenticacaoUsuarioQuery, IEvent>
    {
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public AutenticacaoUsuarioHandler(
            IJwtService jwtService,
            IUsuarioRepository usuariorRepository,
            IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator)
        {
            _jwtService = jwtService;
            _usuarioRepository = usuariorRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task<IEvent> Handle(AutenticacaoUsuarioQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IUsuario usuario = await _usuarioRepository.ObterPorEmailCadastroAtivo(request.Email, cancellationToken);
                if (usuario == null)
                {
                    return new ResultEvent(false, "Verifique se digitou corretamente os dados de acesso e tente novamente.");
                }
                if (!Criptografia.Verify(request.Password, usuario.Senha))
                {
                    return new ResultEvent(false, "Login/Senha inválidos, tente novamente.");
                }
                var jwt = _jwtService.GenerateUsuarioToken(usuario);
                await _refreshTokenRepository.AtualizarPorUsuarioId(jwt.RefreshToken);
                var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                return new ResultEvent(true, new
                {
                    access_token = jwt.AccessToken,
                    refresh_token = jwt.RefreshToken.Token,
                    token_type = jwt.TokenType,
                    expires_in = jwt.ExpiresIn
                });
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
           
        }
    }
}
