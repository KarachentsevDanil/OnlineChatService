using System;

namespace OCS.BLL.Exceptions.Chats
{
    public class PrivateChatAlreadyExistException : Exception
    {
        public PrivateChatAlreadyExistException()
        {
        }

        public PrivateChatAlreadyExistException(string message)
            : base(message)
        {
        }

        public PrivateChatAlreadyExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}