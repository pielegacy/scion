using Scion.Lambda.Common.Interface.Service;

namespace Scion.Lambda.Common
{
    public abstract class BaseFunction
    {
        public IExternalCardService ExternalCardService { get; }

        public BaseFunction()
        {
            ExternalCardService = new ExternalCardService(new Configuration.ExternalCardServiceConfiguration
            {
                BaseUrl = "https://mtgjson.com/api/v5"
            });
        }

    }
}
