using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Scion.Lambda.Common.Extensions;
using Scion.Lambda.Common.Interface.Models.Configuration;
using Scion.Lambda.Common.Interface.Service;
using System.Text.Json;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Service
{
    public sealed class QueueOutputService : IQueueOutputService
    {
        private QueueOutputConfiguration Configuration { get; }
        private ILogger Logger { get; }
        private AmazonSQSClient Client { get; }

        public QueueOutputService(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration.GetQueueOutputConfiguration();
            Logger = logger;
            Client = new AmazonSQSClient();
        }

        public async Task QueueSuccessAsync<TSuccessOutput>(TSuccessOutput output) where TSuccessOutput : notnull
        {
            using var scope = Logger.BeginScope(output);

            await Client.SendMessageAsync(new()
            {
                QueueUrl = Configuration.UrlSuccess,
                MessageBody = JsonSerializer.Serialize(output),
            });

            Logger.LogDebug("Submitted success message to {Url}", Configuration.UrlSuccess);
        }
    }
}
