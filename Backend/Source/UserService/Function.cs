using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo(
    "UserService.Test"
)]

namespace UserService
{
    using System;
    using Amazon.Lambda.Core;
    using Shared;
    using Shared.Container;
    using Shared.Http;
    using Shared.Request;
    using Shared.Response;
    using UserService.LogIn;
    using UserService.Read;
    using UserService.SignUp;
    using UserService.Update;
    using UserService.VerifyEmail;

    /// <summary>
    /// Lambda function entry class
    /// </summary>
    public class Function
    {
        /// <summary>
        /// UserService lambda function handler
        /// </summary>
        /// <param name="request"> Request for lambda handler </param>
        /// <param name="context"> Context info for lambda handler </param>
        /// <returns> Lambda response </returns>
        [LambdaSerializer(typeof(LambdaSerializer))]
        public Response FunctionHandler(Request request, ILambdaContext context)
        {
            var container = new CommandContainer();

            container.Register<LogInCommand>(Operation.LogIn)
                     .Register<ReadCommand>(Operation.Read)
                     .Register<SignUpCommand>(Operation.SignUp)
                     .Register<UpdateCommand>(Operation.Update)
                     .Register<VerifyEmailCommand>(Operation.VerifyEmail);

            try
            {
                return container[request.Operation].Invoke(request);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }
        }
    }
}
