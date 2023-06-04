using System;
using System.Collections.Generic;
using System.Text;

namespace Scion.Lambda.Common.Interface.Models.External
{
    public sealed class SetCard
    {
        public string Name { get; set; } = string.Empty;

        public Identifiers Identifiers { get; set; } = new();
    }
}
