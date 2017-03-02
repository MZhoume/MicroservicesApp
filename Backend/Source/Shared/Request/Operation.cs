namespace Shared.Request
{
    /// <summary>
    /// Types for operation a service can perform, CRUD
    /// </summary>
    public enum Operation
    {
#pragma warning disable CS1591
#pragma warning disable SA1602

        // supported DB operation
        Create,
        Delete,
        Update,
        Read,

        // supported user operation
        SignUp,
        LogIn,
        VerifyEmail

#pragma warning restore SA1602
#pragma warning restore CS1591
    }
}