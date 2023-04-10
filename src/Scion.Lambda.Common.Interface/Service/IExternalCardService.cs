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
        /// Returns a list of sets with metadata details.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SetDetails>> GetSetsAsync();
    }
}