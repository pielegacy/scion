using System.Net.Http;
using Scion.Lambda.Common.Configuration;
using Scion.Lambda.Common.Interface.Service;

namespace Scion.Lambda.Common
{
    public sealed class ExternalCardService : IExternalCardService
    {
        private readonly ExternalCardServiceConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ExternalCardService(ExternalCardServiceConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }
        
    }
}