namespace AuthService
{
    using System;
    using System.Collections.Generic;
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.Core;
    using Jose;
    using Newtonsoft.Json;
    using Static;

    /// <summary>
    /// Authentication Service entry class
    /// </summary>
    public class Function
    {
        private readonly DateTime entryTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Function" /> class
        /// </summary>
        public Function()
        {
            this.entryTime = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Function" /> class, for unit test only
        /// </summary>
        /// <param name="entryTime"> The entry time to be mocked </param>
        public Function(DateTime entryTime)
        {
            this.entryTime = entryTime;
        }

        /// <summary>
        /// Handle the Authentication Request
        /// </summary>
        /// <param name="request"> Authentication Request </param>
        /// <param name="context"> API Gateway Custom Authorizer Context </param>
        /// <returns> API Gateway Custom Authorizer Response </returns>
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public APIGatewayCustomAuthorizerResponse FunctionHandler(
            APIGatewayCustomAuthorizerRequest request,
            ILambdaContext context)
        {
            var token = request.AuthorizationToken;
            var response = new APIGatewayCustomAuthorizerResponse();
            var policy = response.PolicyDocument;
            var statement = new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement()
            {
                Action = new HashSet<string>(),
                Resource = new HashSet<string>()
            };

            try
            {
                var payload = JsonConvert.DeserializeObject<JwtPayload>(JWT.Decode(token, Values.JWTSecretKey));

                if (payload.TimeStamp > this.entryTime.AddMinutes(-5))
                {
                    var vars = request.MethodArn.Split(':');
                    var apiVars = vars[5].Split('/');
                    var region = vars[3];
                    var accountId = vars[4];
                    var apiId = apiVars[0];
                    var stage = apiVars[1];

                    this.AllowOperation(
                            statement,
                            CustomAuthorizerHelper.ComposeAction(Action.Invoke),
                            CustomAuthorizerHelper.ComposeResource(region, accountId, apiId, stage, Resource.Any, Resource.Any));
                }
                else
                {
                    this.DenyAll(statement);
                }
            }
            catch
            {
                this.DenyAll(statement);
            }

            policy.Statement.Add(statement);
            return response;
        }

        /// <summary>
        /// Allow a specific action to the resource
        /// </summary>
        /// <param name="statement"> Statement to modify </param>
        /// <param name="action"> Action to allow </param>
        /// <param name="resource"> Resource to allow </param>
        private void AllowOperation(APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement statement, string action, string resource)
        {
            statement.Effect = Effect.Allow;
            statement.Action.Add(action);
            statement.Resource.Add(resource);
        }

        /// <summary>
        /// Deny all actions to the all the resource
        /// </summary>
        /// <param name="statement"> Statement to modify </param>
        private void DenyAll(APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement statement)
        {
            statement.Effect = Effect.Deny;
            statement.Action.Add(CustomAuthorizerHelper.ComposeAction(Action.All));
            statement.Resource.Add(CustomAuthorizerHelper.ComposeResource(
                Resource.Any, Resource.Any, Resource.Any, Resource.Any, Resource.Any, Resource.Any));
        }
    }
}