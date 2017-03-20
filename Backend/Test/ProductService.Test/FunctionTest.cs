namespace ProductService.Test
{
    using Amazon.Lambda.TestUtilities;
    using ProductService;
    using Xunit;
    using Shared.Request;

    public class FunctionTest
    {
        [Fact]
        public void TestProductServiceFunction()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            // var function = new Function();
            // var context = new TestLambdaContext();
            // var req = new Request();
            // req.Operation = Operation.Read;
            // req.PagingInfo.Count = 1;
            // req.PagingInfo.Start = 0;
            // SearchTerm s = new SearchTerm();
            // //s.Field = "Id";
            // //s.Operator = SearchOperator.LIKE;
            // //req.SearchTerm = {s};
            // var result = function.FunctionHandler(req, context);

            // Assert.Equal(null, result);
        }
    }
}
