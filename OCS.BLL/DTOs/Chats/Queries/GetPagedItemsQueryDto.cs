namespace OCS.BLL.DTOs.Chats.Queries
{
    public class GetPagedMessagesQueryDto
    {
        public int ChatId { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}