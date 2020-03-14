using System.Text.Json.Serialization;

namespace OCS.BLL.DTOs.Chats.Private
{
    public class GetPrivateChatsQueryDto
    {
        public string UserTerm { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}