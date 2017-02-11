namespace AuthService
{
    using System.Collections.Generic;
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.Core;
    using Jose;
    using Static;

    /// <summary>
    /// Authentication Service entry class
    /// </summary>
    public class Function
    {
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
            var statement = new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement();
            statement.Action = new HashSet<string>();
            statement.Resource = new HashSet<string>();

            try
            {
                JWT.Decode(token, Values.JWTSecretKey);
                statement.Effect = Effect.Allow;
                statement.Action.Add(CustomAuthorizerHelper.ComposeAction(Action.Invoke));

                var vars = request.MethodArn.Split(':');
                var apiVars = vars[5].Split('/');
                var region = vars[3];
                var accoundId = vars[4];
                var apiId = apiVars[0];
                var stage = apiVars[1];
                statement.Resource.Add(CustomAuthorizerHelper.ComposeResource(
                    region, accoundId, apiId, stage, Resource.Any, Resource.Any));
            }
            catch
            {
                statement.Effect = Effect.Deny;
                statement.Action.Add(CustomAuthorizerHelper.ComposeAction(Action.All));

                statement.Resource.Add(CustomAuthorizerHelper.ComposeResource(
                    Resource.Any, Resource.Any, Resource.Any, Resource.Any, Resource.Any, Resource.Any));
            }

            policy.Statement.Add(statement);
            return response;
        }
    }
}
