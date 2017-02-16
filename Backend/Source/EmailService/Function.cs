namespace EmailService
{
    using Amazon.Lambda.Core;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Example lambda function handler
        /// </summary>
        /// <param name="input"> Input for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Value send to clients </returns>
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public string FunctionHandler(string input, ILambdaContext context)
        {
            return input?.ToUpper();
        }
    }
}
