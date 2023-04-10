using System;
using System.Collections.Generic;
using System.Text;

namespace Scion.Lambda.Common.Interface.Models.External
{
    public sealed class SetList
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.MinValue;
    }
}
