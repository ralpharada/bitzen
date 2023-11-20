using Bitzen.Application.Contracts;
using Bitzen.Application.Core;
using Bitzen.Application.Services;
using Bitzen.Domain.Interfaces;
using Bitzen.Infra.Repositories;

namespace Bitzen.API.DependencyMap
{
    public static class RepositoryDependencyMap
    {
        public static void RepositoryMap(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<UsuarioAutenticado>();
            services.AddSingleton<IJwtService,JwtService>();
            services.AddScoped<Application.Libraries.Cookie>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICombustivelRepository, CombustivelRepository>();
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IAbastecimentoRepository, AbastecimentoRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
