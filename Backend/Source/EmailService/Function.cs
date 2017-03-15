using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "EmailService.Test"
)]

namespace EmailService
{
    using Amazon.Lambda.Core;
    using Amazon.Lambda.SNSEvents;
    using Shared;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Example lambda function handler
        /// </summary>
        /// <param name="snsEvent"> SNS Event for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public string FunctionHandler(SNSEvent snsEvent, ILambdaContext context)
        {
            return null;
        }
    }
}
