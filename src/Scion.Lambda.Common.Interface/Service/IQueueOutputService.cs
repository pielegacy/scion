using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scion.Lambda.Common.Interface.Service
{
    public interface IQueueOutputService
    {
        Task QueueSuccessAsync<TSuccessOutput>(TSuccessOutput output) where TSuccessOutput : notnull;
    }
}
