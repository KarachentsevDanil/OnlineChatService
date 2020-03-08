using System;

namespace OCS.BLL.Exceptions.Users
{
    public class UserRegistrationFailedException : Exception
    {
        public UserRegistrationFailedException()
        {
        }

        public UserRegistrationFailedException(string message)
            : base(message)
        {
        }

        public UserRegistrationFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}