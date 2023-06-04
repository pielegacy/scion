using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace Scion.Lambda.Common.Interface.Models.External
{
    public sealed class SetDetails
    {
        public IEnumerable<SetCard> Cards { get; set; } = Enumerable.Empty<SetCard>();
    }
}
