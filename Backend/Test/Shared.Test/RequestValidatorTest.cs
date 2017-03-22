namespace Shared.Test
{
    using System.ComponentModel.DataAnnotations;
    using Shared.Validation;
    using Shared.Model;
    using Shared.Request;
    using Xunit;

    public class RequestValidatorTest
    {
        [Fact]
        public void ValidatorShouldSuccessForValidField()
        {
            var r = new Request()
            {
                Operation = Operation.Create,
                SearchTerm = new[] {
                    new SearchTerm()
                    {
                        Field = "Field",
                        Operator = SearchOperator.EQ,
                        Value = "value"
                    }
                }
            };

            r.Validate();
        }

        [Fact]
        public void ValidatorShouldFailForInvalidField()
        {
            var r = new Request()
            {
                Operation = Operation.Create,
                SearchTerm = new[] {
                    new SearchTerm()
                    {
                        Field = "Field",
                        Operator = (SearchOperator)100,
                        Value = "value"
                    }
                }
            };

            Assert.Throws(typeof(ValidationException), () => r.Validate());
        }
    }
}