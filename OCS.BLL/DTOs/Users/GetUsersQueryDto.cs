using System.Text.Json.Serialization;

namespace OCS.BLL.DTOs.Users
{
    public class GetUsersQueryDto
    {
        public string Term { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}