using System;

namespace Scion.Lambda.Common.Interface.Models.External
{
    public sealed class ResponseContainer<TResponseData> where TResponseData : class
    {
        public sealed class ResponseMetaData
        {
            public DateTime Date { get; set; }
            public string Version { get; set; } = string.Empty;
        }

        public ResponseMetaData Meta { get; set; } = new ResponseMetaData();

        public TResponseData? Data { get; set; } = default;
    }
}
