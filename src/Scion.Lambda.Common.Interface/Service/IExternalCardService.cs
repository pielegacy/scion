using Scion.Lambda.Common.Interface.Models;
using Scion.Lambda.Common.Interface.Models.External;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Interface.Service
{
    /// <summary>
    /// Used to pull card data from an external service.
    /// </summary>
    /// <remarks>Currently the external service used for card data is MTGJson</remarks>
    public interface IExternalCardService
    {
        /// <summary>
        /// Returns all the cards in a specific set
        /// </summary>
        /// <param name="inputCode">The set code to search by</param>
        /// <returns></returns>
        Task<IEnumerable<Card>> GetCardsAsync(string inputCode);

        /// <summary>
        /// Returns a list of sets with metadata details.
        /// </summary>
        /// <param name="filter">The filter to apply to the sets retrieved</param>
        /// <returns></returns>
        Task<IEnumerable<SetMeta>> GetSetsAsync(SetMetaFilter filter);
    }
}