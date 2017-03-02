namespace Static.Test
{
    using Static.Http;
    using Xunit;

    public class LambdaExceptionTest
    {
        [Fact]
        public void LambdaExceptionShouldContainCorrectMessage()
        {
            var ex = new LambdaException(HttpCode.BadRequest, "Testing...");
            Assert.Equal("[400] | Testing...", ex.Message);
        }
    }
}