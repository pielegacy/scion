using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scion.Lambda.Common.Configuration;
using Scion.Lambda.Common.Interface.Service;
using Scion.Lambda.Common.Interface.Service.Data;
using Scion.Lambda.Common.Service;
using Scion.Lambda.Common.Service.Data;
using System;

namespace Scion.Lambda.Common
{
    public abstract class BaseFunction
    {
        protected IConfiguration Configuration { get; set; }
        protected ILogger Logger { get; }
        protected IExternalCardService ExternalCardService { get; }
        protected IQueueOutputService QueueOutputService { get; }

        public BaseFunction()
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(Environment.CurrentDirectory)
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();

            Logger = LoggerFactory
                .Create(options => options.AddJsonConsole())
                .CreateLogger(GetType().Name);

            var externalCardServiceConfiguration = Configuration
                .GetSection(ExternalCardServiceConfiguration.ConfigurationSection)
                .Get<ExternalCardServiceConfiguration>()
                ?? throw new ArgumentException($"No configuration provided in appsettings section '${ExternalCardServiceConfiguration.ConfigurationSection}'");

            ExternalCardService = new ExternalCardService(externalCardServiceConfiguration);
            QueueOutputService = new QueueOutputService(Configuration, Logger);
        }

        protected IChaliceRepository CreateRepository() => new ChaliceRepository(Configuration);
    }
}
