using Riok.Mapperly.Abstractions;
using Scion.Lambda.Common.Interface.Models;
using Scion.Lambda.Common.Interface.Models.External;
using System.Collections.Generic;

namespace Scion.Lambda.Common.Mapping
{
    [Mapper]
    public partial class ExternalDataMapper
    {
        public partial IEnumerable<SetDetails> ToSetDetails(IEnumerable<SetList> setList);
    }
}
