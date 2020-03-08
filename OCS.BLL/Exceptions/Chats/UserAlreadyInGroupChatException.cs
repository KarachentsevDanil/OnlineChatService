using System;

namespace OCS.BLL.Exceptions.Chats
{
    public class UserAlreadyInGroupChatException : Exception
    {
        public UserAlreadyInGroupChatException()
        {
        }

        public UserAlreadyInGroupChatException(string message)
            : base(message)
        {
        }

        public UserAlreadyInGroupChatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}