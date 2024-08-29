namespace MyExpenses.Domain.core.Entities.Enums
{
    /// <summary>
    /// Represents authentication status codes.
    /// </summary>
    public enum AuthCode
    {
        /// <summary>
        /// The user does not exist.
        /// </summary>
        UserDoesNotExist = 0,

        /// <summary>
        /// The user is already registered.
        /// </summary>
        AlreadyRegistered = 1,

        /// <summary>
        /// The login attempt failed.
        /// </summary>
        LoginFailed = 2,

        /// <summary>
        /// The password provided is invalid.
        /// </summary>
        InvalidPassword = 3,

        /// <summary>
        /// An unknown error occurred.
        /// </summary>
        UnknownError = 4,

        /// <summary>
        /// General failure.
        /// </summary>
        Failure = 99,

        /// <summary>
        /// Authentication was successful.
        /// </summary>
        Success = 100
    }

}
