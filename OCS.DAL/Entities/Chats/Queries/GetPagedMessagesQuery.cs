namespace OCS.DAL.Entities.Chats.Queries
{
    public class GetPagedMessagesQuery
    {
        public int ChatId { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}