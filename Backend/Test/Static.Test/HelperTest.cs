namespace Static.Test
{
    using Static;
    using Static.Request;
    using Xunit;

    public class HelperTest
    {
        [Fact]
        public void GetJwtPayloadShouldReturnCorrectJwtPayload()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjAsIkVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwiRmlyc3ROYW1lIjoiQWRtaW4iLCJMYXN0TmFtZSI6IkFkbWluIn0.q4G1zil1ouFVwhF1oEeD6CNA_j25kYNYDQMUuYVbMyY";

            var payload = Helper.GetJwtPayload(token);
            Assert.Equal(0, payload.UserId);
            Assert.Equal("admin@admin.com", payload.Email);
            Assert.Equal("Admin", payload.FirstName);
            Assert.Equal("Admin", payload.LastName);
        }

        [Fact]
        public void GetJwtTokenShouldReturnCorrectJwtToken()
        {
            var payload = new JwtPayload()
            {
                UserId = 0,
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin"
            };

            var token = Helper.GetJwtToken(payload);
            Assert.Equal("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOjAsIkVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwiRmlyc3ROYW1lIjoiQWRtaW4iLCJMYXN0TmFtZSI6IkFkbWluIn0.q4G1zil1ouFVwhF1oEeD6CNA_j25kYNYDQMUuYVbMyY", token);
        }

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

            var exp = Helper.ComposeWhereExp(terms);
            Assert.Equal("Id <= 5 AND Email.Contains(\"2\") AND Time > DateTime.Parse(\"Jan 1, 2001\")", exp);
        }
    }
}