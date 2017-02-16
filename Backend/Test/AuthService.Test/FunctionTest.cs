namespace AuthService.Test
{
    using System;
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.TestUtilities;
    using AuthService;
    using Jose;
    using Static;
    using Xunit;

    public class FunctionTest
    {
        private readonly ILambdaContext context = new TestLambdaContext();

        private readonly DateTime tokenTime = new DateTime(2000, 1, 1, 0, 1, 1);
        private readonly JwtPayload payload = new JwtPayload()
            {
                UserId = 0,
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin"
            };

        private readonly string token;

        public FunctionTest()
        {
            this.payload.TimeStamp = this.tokenTime;
            this.token = JWT.Encode(this.payload, Values.JWTSecretKey, JwsAlgorithm.HS256);
        }

        [Fact]
        public void AuthSuccessForTokenWithin5Min()
        {
            var validRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = this.token,
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var function = new Function(this.tokenTime.AddMinutes(1));
            var validPolicy = function.FunctionHandler(validRequest, this.context).PolicyDocument;

            Assert.Equal(validPolicy.Statement[0].Effect, Effect.Allow);
            Assert.True(validPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(AuthService.Action.Invoke)));
            Assert.True(validPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/*/*"));
        }

        [Fact]
        public void AuthFailForTokenOlderThan5Min()
        {
            var validRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = this.token,
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var function = new Function(this.tokenTime.AddMinutes(6));
            var invalidPolicy = function.FunctionHandler(validRequest, this.context).PolicyDocument;

            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny);
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(AuthService.Action.All)));
            Assert.True(invalidPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:*:*:*/*/*/*"));
        }

        [Fact]
        public void AuthFailForInvalidTokenWithin5Min()
        {
            var invalidRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "invalid",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var function = new Function(this.tokenTime.AddMinutes(1));
            var invalidPolicy = function.FunctionHandler(invalidRequest, this.context).PolicyDocument;

            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny);
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(AuthService.Action.All)));
            Assert.True(invalidPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:*:*:*/*/*/*"));
        }

        [Fact]
        public void AuthFailForInvalidTokenOlderThan5Min()
        {
            var invalidRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "invalid",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var function = new Function(this.tokenTime.AddMinutes(6));
            var invalidPolicy = function.FunctionHandler(invalidRequest, this.context).PolicyDocument;

            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny);
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(AuthService.Action.All)));
            Assert.True(invalidPolicy.Statement[0].Resource.Contains("arn:aws:execute-api:*:*:*/*/*/*"));
        }
    }
}
