namespace AuthService.Test
{
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.TestUtilities;
    using AuthService;
    using Static.Request;
    using Xunit;

    public class FunctionTest
    {
        private readonly JwtPayload payload = new JwtPayload()
            {
                UserId = 0,
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin"
            };

        [Fact]
        public void AuthSuccessWithValidToken()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            var validRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = RequestHelper.GetJwtToken(this.payload),
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var validPolicy = function.FunctionHandler(validRequest, context).PolicyDocument;

            Assert.Equal(validPolicy.Statement[0].Effect, Effect.Allow);
            Assert.True(validPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(AuthService.Action.Invoke)));
            Assert.True(validPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/*/*"));
        }

        [Fact]
        public void AuthFailForInvalidTokenWithin5Min()
        {
            var function = new Function();
            var context = new TestLambdaContext();

            var invalidRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "invalid",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var invalidPolicy = function.FunctionHandler(invalidRequest, context).PolicyDocument;

            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny);
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(AuthService.Action.All)));
            Assert.True(invalidPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:*:*:*/*/*/*"));
        }
    }
}
