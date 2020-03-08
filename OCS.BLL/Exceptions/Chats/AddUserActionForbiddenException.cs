using System;

namespace OCS.BLL.Exceptions.Chats
{
    public class AddUserActionForbiddenException : Exception
    {
        public AddUserActionForbiddenException()
        {
        }

        public AddUserActionForbiddenException(string message)
            : base(message)
        {
        }

        public AddUserActionForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}