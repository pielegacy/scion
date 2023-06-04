using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scion.Lambda.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Artificer
{
    public static class Extensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration) => serviceCollection
            .AddFluentMigratorCore()
            .AddSingleton(configuration)
            .ConfigureRunner((runnerBuilder) => runnerBuilder
                .AddPostgres11_0()
                .WithGlobalConnectionString(configuration.GetChaliceConfiguration().ConnectionString)
                .ScanIn(typeof(Extensions).Assembly).For.Migrations()
            );
    }
}
