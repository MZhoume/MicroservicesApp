namespace Shared.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Shared.Validation;
    using Shared.Model;
    using Xunit;

    public class ValidationTest
    {
        [Fact]
        public void ValidatorShouldSuccessForValidField()
        {
            var u = new User()
            {
                Id = 10,
                Email = "test@test.com",
                PwdHash = "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy",
                FirstName = "Test",
                LastName = "Test"
            };

            Assert.Equal(u, u.Validate());
        }

        [Fact]
        public void ValidatorShouldFailForInvalidField()
        {
            var u = new User()
            {
                Id = 1,
                Email = "test@test.com",
                PwdHash = "0",
                FirstName = "Test",
                LastName = "Test"
            };

            Assert.Throws(typeof(ValidationException), () => u.Validate());
        }
    }
}