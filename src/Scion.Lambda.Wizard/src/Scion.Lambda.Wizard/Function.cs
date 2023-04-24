using Amazon.Lambda.Core;
using Scion.Lambda.Common;
using Scion.Lambda.Common.Interface.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
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
        public string FunctionHandler(FunctionInput input, ILambdaContext context)
        {
            return input.Code;
        }

        public sealed class FunctionInput : SetDetails
        {

        }
    }
}