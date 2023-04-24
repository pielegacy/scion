using Amazon.Lambda.Core;
using Amazon.SQS;
using Scion.Lambda.Common;
using Scion.Lambda.Common.Interface.Models;
using System.Text.Json;
using System.Collections;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Scion.Lambda.Advisor
{
    public class Function : BaseFunction
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SetDetails>> FunctionHandler(FunctionInput input, ILambdaContext context)
        {
            var setLists = await ExternalCardService.GetSetsAsync(new SetDetailsFilter
            {
                After = input.After,
            });

            foreach (var set in setLists)
            {
                await QueueOutputService.QueueSuccessAsync(set);
            }

            return setLists;
        }

        public sealed class FunctionInput
        {
            public DateTime? After { get; set; }
        }
    }
}