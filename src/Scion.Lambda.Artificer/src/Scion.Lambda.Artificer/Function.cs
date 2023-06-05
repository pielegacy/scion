using Amazon.Lambda.Core;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Scion.Lambda.Common;
using Scion.Lambda.Common.Extensions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Scion.Lambda.Artificer
{
    public sealed class Function : BaseFunction
    {
        public IMigrationRunner MigrationRunner { get; }

        public Function() : base()
        {
            var services = new ServiceCollection()
                .RegisterServices(Configuration)
                .BuildServiceProvider();

            MigrationRunner = services.GetRequiredService<IMigrationRunner>();
        }

        public void FunctionHandler(FunctionInput input, ILambdaContext context)
        {
            MigrationRunner.MigrateUp();
        }

        public sealed class FunctionInput
        {
        }
    }
}