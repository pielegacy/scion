using System;
using System.Collections.Generic;
using System.Text;

namespace Scion.Lambda.Common.Interface.Models.Configuration
{
    public sealed class QueueOutputConfiguration
    {
        public const string Section = "QueueOutput";

        public string UrlSuccess { get; set; } = string.Empty;
    }
}
