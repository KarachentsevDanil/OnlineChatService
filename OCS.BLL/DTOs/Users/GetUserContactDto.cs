namespace OCS.BLL.DTOs.Users
{
    public class GetUserContactDto
    {
        public string Id { get; set; }

        public GetUserDto User { get; set; }

        public GetUserDto Contact { get; set; }
    }
}