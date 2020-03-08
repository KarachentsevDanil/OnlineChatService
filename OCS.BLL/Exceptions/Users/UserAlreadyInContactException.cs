using System;

namespace OCS.BLL.Exceptions.Users
{
    public class UserAlreadyInContactException : Exception
    {
        public UserAlreadyInContactException()
        {
        }

        public UserAlreadyInContactException(string message)
            : base(message)
        {
        }

        public UserAlreadyInContactException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}