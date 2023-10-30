using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pmesp_Api_Template2.Application.Interfaces;
using Pmesp_Api_Template2.Application.Services;
using Pmesp_Api_Template2.Domain.Interfaces;
using Pmesp_Api_Template2.infra.Connection;
using Pmesp_Api_Template2.infra.Repositories;

namespace Pmesp_Api_Template2.IoC
{
    public static class ContainerConfigurator
    {
        public static void AddDependencias(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<ConnectionStrings>();

            //Injeção de dependência dos repositórios
            serviceCollection.AddSingleton<IPessoaRepository, PessoaRepository>();
            serviceCollection.AddSingleton<IAutenticacaoRepository, AutenticacaoRepository>();

            //Injeção de dependência dos serviços
            serviceCollection.AddSingleton<IPessoaService, PessoaService>();
            serviceCollection.AddSingleton<IAutenticacaoService, AutenticacaoService>();

            serviceCollection.AddHealthCheckUri(configuration);
        }
    }
}
