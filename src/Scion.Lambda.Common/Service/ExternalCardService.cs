using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            public const string GetSetContents = "/{0}.json";

            public const string GetSetLists = "/SetList.json";

        }

        public ExternalCardService(ExternalCardServiceConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(10),
            };
            _mapper = new ExternalDataMapper();
        }

        public async Task<IEnumerable<SetMeta>> GetSetsAsync(SetMetaFilter filter)
        {
            var setLists = await GetExternalDataAsync<IEnumerable<SetList>>(ExternalPaths.GetSetLists);
            return _mapper.ToSetMetaList(ApplyFilter(filter, setLists));
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(string inputCode)
        {
            var setDetails = await GetExternalDataAsync<SetDetails>(string.Format(ExternalPaths.GetSetContents, inputCode));
            
            return _mapper.ToCards(setDetails.Cards);
        }

        public async Task<Stream> GetCardsAsStreamAsync(string inputCode)
        {
            return await GetExternalDataStreamAsync(string.Format(ExternalPaths.GetSetContents, inputCode));
        }

        private IEnumerable<SetList> ApplyFilter(SetMetaFilter filter, IEnumerable <SetList> setLists)
        {
            var result = setLists;

            if (filter.After.HasValue)
            {
                result = result.Where(setList => setList.ReleaseDate >  filter.After.Value);
            }

            return result;
        }

        private string FormatUrl(string path)
        {
            if (path[0] != '/')
            {
                throw new ArgumentException("External data request paths must start with a '/'", nameof(path));
            }

            return _configuration.BaseUrl + path;
        }

        private async Task<Stream> GetExternalDataStreamAsync(string path)
        {
            string url = FormatUrl(path);
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync();
        }

        private async Task<TData> GetExternalDataAsync<TData>(string path) where TData : class
        {
            string url = FormatUrl(path);
            var responseContainer = await _httpClient.GetFromJsonAsync<ResponseContainer<TData>>(url);
            if (responseContainer?.Data is null)
            {
                throw new ExternalDataMissingException(url);
            }

            return responseContainer.Data;
        }
    }
}