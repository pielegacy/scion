using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Interface.Models.External
{
    public sealed class Identifiers
    {
        [JsonPropertyName("mtgJsonV4Id")]
        public Guid ExternalId { get; set; } = Guid.Empty;
    }
}
