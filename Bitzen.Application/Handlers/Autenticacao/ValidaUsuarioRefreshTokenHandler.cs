using Bitzen.Application.Contracts;
using Bitzen.Application.Queries;
using Bitzen.Core.Events;
using Bitzen.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bitzen.Application.Handlers.Autenticacao
{
    public class ValidaUsuarioRefreshTokenHandler : IRequestHandler<ValidaUsuarioRefreshTokenQuery, IEvent>
    {
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public ValidaUsuarioRefreshTokenHandler(
            IJwtService jwtService,
            IUsuarioRepository usuarioRepository,
            IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator)
        {
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task<IEvent> Handle(ValidaUsuarioRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IUsuario usuario = null;
                if (!String.IsNullOrEmpty(request.RefreshToken))
                {
                    var token = await _refreshTokenRepository.ObterPorChaveUsuario(request.RefreshToken);
                    if (token == null)
                        return new ResultEvent(false, "Refresh Token inválido");
                    if (token.DataExpiracao < DateTime.Now)
                        return new ResultEvent(false, "Refresh Token expirado");
                    usuario = await _usuarioRepository.ObterPorId(token.UsuarioId, new CancellationToken());
                }
                if (usuario != null)
                {
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
                return new ResultEvent(false, "Refresh Token expirado");
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
