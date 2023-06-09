using Scion.Lambda.Common.Interface.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Interface.Service.Data
{
    public interface IChaliceRepository : IDisposable
    {
        Task SaveCardsAsync(IEnumerable<Card> cards);
        Task SaveSetCardsAsync(string setCode, Stream data);
    }
}
