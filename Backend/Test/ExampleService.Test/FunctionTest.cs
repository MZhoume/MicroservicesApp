namespace ExampleService.Test
{
    using Amazon.Lambda.TestUtilities;
    using ExampleService;
    using Xunit;

    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function();
            var context = new TestLambdaContext();
            var upperCase = function.FunctionHandler("hello world", context);

            goto test;
            Assert.True(false);
test:            Assert.Equal("HELLO WORLD", upperCase);
        }
    }
}
