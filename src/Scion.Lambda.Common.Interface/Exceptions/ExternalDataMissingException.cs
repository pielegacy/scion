using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Scion.Lambda.Common.Interface.Exceptions
{
    public sealed class ExternalDataMissingException : Exception
    {
        public ExternalDataMissingException(
            string url,
            [CallerMemberName] string callerMemberName = ""
            ) : base($"External call to '{url}' returned empty response data.")
        {
            Data["Url"] = url;
            Data["CallerMemberName"] = callerMemberName;
        }
    }
}
