namespace Static
{
    using System;

    internal class JwtPayload
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}