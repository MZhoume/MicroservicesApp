namespace AuthService.Test
{
    using Amazon.Lambda.APIGatewayEvents;
    using Amazon.Lambda.Core;
    using Amazon.Lambda.TestUtilities;
    using AuthService;
    using Xunit;

    public class FunctionTest
    {
        private Function function = new Function();
        private ILambdaContext context = new TestLambdaContext();

        [Fact]
        public void TestAuthFunctionSuccess()
        {
            var validRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjAsIkZpcnN0TmFtZSI6IkFkbWluIiwiTGFzdE5hbWUiOiJBZG1pbiJ9.xGO4pOrx1HWHoairSmtL556Bu2h6Sot402ySVZttgrM",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var validPolicy = this.function.FunctionHandler(validRequest, this.context).PolicyDocument;
            Assert.True(validPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(Action.Invoke)));
            Assert.Equal(validPolicy.Statement[0].Effect, Effect.Allow);
        }

        [Fact]
        public void TestAuthFunctionFail()
        {
            var invalidRequest = new APIGatewayCustomAuthorizerRequest()
            {
                AuthorizationToken = "invalid",
                MethodArn = "arn:aws:execute-api:<regionId>:<accountId>:<apiId>/<stage>/<method>/<resourcePath>"
            };

            var invalidPolicy = this.function.FunctionHandler(invalidRequest, this.context).PolicyDocument;
            Assert.True(invalidPolicy.Statement[0].Action.Contains(CustomAuthorizerHelper.ComposeAction(Action.All)));
            Assert.Equal(invalidPolicy.Statement[0].Effect, Effect.Deny);
        }
    }
}
