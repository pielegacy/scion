using Riok.Mapperly.Abstractions;
using Scion.Lambda.Common.Interface.Models;
using Scion.Lambda.Common.Interface.Models.External;
using System.Collections.Generic;

namespace Scion.Lambda.Common.Mapping
{
    [Mapper]
    public partial class ExternalDataMapper
    {
        public partial IEnumerable<SetMeta> ToSetMetaList(IEnumerable<SetList> setList);

        [MapProperty("Identifiers.ExternalId", "Id")]
        public partial Card ToCard(SetCard card);

        public partial IEnumerable<Card> ToCards(IEnumerable<SetCard> cards);
    }
}
