namespace ProductService.Test
{
    using Amazon.Lambda.TestUtilities;
    using ProductService;
    using Xunit;
    using Shared.Model;
    using System;
    using Shared.Request;

    public class FunctionTest
    {
        [Fact]
        public void TestProductServiceFunction()
        {
            //Invoke the lambda function.
            var function = new Function();
            var context = new TestLambdaContext();
            var req = new Request(){
                AuthToken = "Token",
                Operation = Operation.Read,
                Payload = null,
                PagingInfo = new PagingInfo(){
                    Start = 0,
                    Count = 10
                },
                SearchTerm = new[] {
                    new SearchTerm(){
                        Field = "Id",
                        Operator = SearchOperator.EQ,
                        Value = "1"
                    }
                }
            };
            var res = function.FunctionHandler(req, context);
            Assert.Equal(1, ((Shared.Model.Product[])res.Payload)[0].Id);
        }
    }
}
