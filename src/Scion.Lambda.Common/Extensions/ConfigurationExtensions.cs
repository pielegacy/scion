using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Scion.Lambda.Common.Interface.Models.Configuration;
using Scion.Lambda.Common.Service.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static QueueOutputConfiguration GetQueueOutputConfiguration(this IConfiguration configuration)
        {
            var options = configuration.GetSection(QueueOutputConfiguration.Section).Get<QueueOutputConfiguration>();
            if (options is null)
            {
                throw new ArgumentException($"The provided configuration does not contain a '{QueueOutputConfiguration.Section}'", nameof(configuration));
            }

            return options;
        }

        public static ChaliceConfiguration GetChaliceConfiguration(this IConfiguration configuration)
        {
            var options = configuration.GetSection(ChaliceConfiguration.Section).Get<ChaliceConfiguration>();
            if (options is null)
            {
                throw new ArgumentException($"The provided configuration does not contain a '{ChaliceConfiguration.Section}'", nameof(configuration));
            }

            return options;
        }
    }
}
