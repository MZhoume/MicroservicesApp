namespace Static.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Static.Validation;
    using Static.Model;
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