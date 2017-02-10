namespace AuthService.Test
{
    using Amazon.Lambda.TestUtilities;
    using Amazon.Lambda.APIGatewayEvents;
    using AuthService;
    using Xunit;

    public class FunctionTest
    {
        [Fact]
        public void TestAuthFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();

            var validRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjAsIkZpcnN0TmFtZSI6IkFkbWluIiwiTGFzdE5hbWUiOiJBZG1pbiJ9.xGO4pOrx1HWHoairSmtL556Bu2h6Sot402ySVZttgrM",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var validPolicy = function.FunctionHandler(validRequest, context).PolicyDocument;
            Assert.True(validPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(Action.Invoke)));
            Assert.Equal(validPolicy.Statement[0].Effect, Effect.Allow);

            var invalidRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "invalid",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var invalidPolicy = function.FunctionHandler(invalidRequest, context).PolicyDocument;
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(Action.All)));
            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny);
        }
    }
}
