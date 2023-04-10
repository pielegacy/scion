using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Scion.Lambda.Common.Configuration;
using Scion.Lambda.Common.Interface.Service;
using System;
using System.IO;

namespace Scion.Lambda.Common
{
    public abstract class BaseFunction
    {
        public IConfiguration Configuration { get; set; }

        public IExternalCardService ExternalCardService { get; }

        public BaseFunction()
        {
            Configuration = new ConfigurationBuilder()
                            .SetBasePath(Environment.CurrentDirectory)
                            .AddJsonFile("appsettings.json")
                            .Build();

            var externalCardServiceConfiguration = Configuration
                .GetSection(ExternalCardServiceConfiguration.ConfigurationSection)
                .Get<ExternalCardServiceConfiguration>()
                ?? throw new ArgumentException($"No configuration provided in appsettings section '${ExternalCardServiceConfiguration.ConfigurationSection}'");

            ExternalCardService = new ExternalCardService(externalCardServiceConfiguration);
        }

    }
}
