using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Service.Configuration
{
    public sealed class ChaliceConfiguration
    {
        public const string Section = "Chalice";

        public string ConnectionString { get; set; } = string.Empty;
    }
}
