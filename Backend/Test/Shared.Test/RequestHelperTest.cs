namespace Shared.Test
{
    using Shared.Request;
    using Xunit;

    public class HelperTest
    {
        [Fact]
        public void ComposeWhereExpShouldReturnCorrectValue()
        {
            var terms = new[]
            {
                new SearchTerm()
                {
                    Field = "Id", Operator = SearchOperator.LE, Value = "5"
                },
                new SearchTerm()
                {
                    Field = "Email", Operator = SearchOperator.LIKE, Value = "2"
                },
                new SearchTerm()
                {
                    Field = "Time", Operator = SearchOperator.GT, Value = "Jan 1, 2001"
                }
            };

            var exp = RequestHelper.ComposeWhereExp(terms);
            Assert.Equal("Id <= 5 AND Email.Contains(\"2\") AND Time > DateTime.Parse(\"Jan 1, 2001\")", exp);
        }
    }
}