using System;

namespace OCS.BLL.Exceptions.Users
{
    public class UserLoginFailedException : Exception
    {
        public UserLoginFailedException()
        {
        }

        public UserLoginFailedException(string message)
            : base(message)
        {
        }

        public UserLoginFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}