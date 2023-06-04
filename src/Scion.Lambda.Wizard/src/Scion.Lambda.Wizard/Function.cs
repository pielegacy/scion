using Amazon.Lambda.Core;
using Microsoft.Extensions.Logging;
using Scion.Lambda.Common;
using Scion.Lambda.Common.Interface.Models;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace Scion.Lambda.Wizard
{
    public class Function : BaseFunction
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(FunctionInput input, ILambdaContext context)
        {
            Logger.LogInformation("Processing set {0}", input.Code);

            var cards = await ExternalCardService.GetSetCardsAsync(input.Code);

            return input.Code;
        }

        public sealed class FunctionInput : SetMeta { }
    }
}