using ArcadeScore.Application.Interfaces;
using ArcadeScore.Application.Services;
using ArcadeScore.Domain.Interfaces;
using ArcadeScore.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeScore.Infra.Ioc
{
    // Configura e registra as dependências da aplicação
    public static class DependencyInjection
    {
        // IServiceCollection é a lista de serviços da aplicação
        // AddInfrascructure é o nome do método (podemos escolher)
        // this => significa q é um método de extensão, ou seja, no program.cs, vamos poder escrever builder.Services.AddInfrastructure();
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            // Quando alguém pedir um reposítório, entregue um ScoreRepository
            // Singleton => Uma única vez, e é reaproveitado enquanto a API estiver rodando
            // O repositório tem que ser Singleton, pois os dados ficam guardados dentro dele, se ele fosse Scoped, ele à cada requisição,
            // receberia um repositório novo e os dados seriam perdidos
            services.AddSingleton<IScoreRepository, ScoreRepository>();

            // Quando alguém pedir um serviço, entregue um ScoreService
            // Scoped => cria um novo objeto a cada requisição (cada chamada à API)
            services.AddScoped<IScoreService, ScoreService>();

            return services;
        }
    }
}
