namespace Shared.Test
{
    using Shared.EnumHelper;
    using Xunit;

    public class AttributeTest
    {
        private enum Test
        {
            [StringValue("Test")]
            Test
        }

        [Fact]
        public void EnumStringValueTest()
        {
            Assert.Equal("Test", Test.Test.GetStringValue());
        }
    }
}