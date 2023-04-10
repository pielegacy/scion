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
        /// Returns all the set lists which can be used for viewing metadata about sets.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SetList>> GetSetListsAsync();
    }
}