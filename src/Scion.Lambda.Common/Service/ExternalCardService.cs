using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Scion.Lambda.Common.Configuration;
using Scion.Lambda.Common.Interface.Exceptions;
using Scion.Lambda.Common.Interface.Models;
using Scion.Lambda.Common.Interface.Models.External;
using Scion.Lambda.Common.Interface.Service;
using Scion.Lambda.Common.Mapping;

namespace Scion.Lambda.Common
{
    public sealed class ExternalCardService : IExternalCardService
    {
        private readonly ExternalCardServiceConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ExternalDataMapper _mapper;

        private static class ExternalPaths
        {
            public const string GetSetLists = "/SetList.json";
        }

        public ExternalCardService(ExternalCardServiceConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _mapper = new ExternalDataMapper();
        }

        public async Task<IEnumerable<SetDetails>> GetSetsAsync() => 
            _mapper.ToSetDetails(await GetExternalData<IEnumerable<SetList>>(ExternalPaths.GetSetLists));

        private async Task<TData> GetExternalData<TData>(string path) where TData : class
        {
            if (path[0] != '/')
            {
                throw new ArgumentException("External data request paths must start with a '/'", nameof(path));
            }

            string url = _configuration.BaseUrl + path;
            var responseContainer = await _httpClient.GetFromJsonAsync<ResponseContainer<TData>>(url);
            if (responseContainer?.Data is null)
            {
                throw new ExternalDataMissingException(url);
            }

            return responseContainer.Data;
        }
    }
}