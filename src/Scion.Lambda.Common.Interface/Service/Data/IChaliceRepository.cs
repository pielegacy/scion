using Scion.Lambda.Common.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Interface.Service.Data
{
    public interface IChaliceRepository : IDisposable
    {
        Task PurgeCardsAsync();
        Task SaveCardsAsync(IEnumerable<Card> cards);
    }
}
