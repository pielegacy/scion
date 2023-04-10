using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Scion.Lambda.Common.Configuration
{
    public sealed class ExternalCardServiceConfiguration 
    {
        public const string ConfigurationSection = "ExternalCardService";

        public string BaseUrl { get; set; } = string.Empty;

        public IEnumerable<string> SupportSetTypes { get; set; } = Enumerable.Empty<string>();
    }
}