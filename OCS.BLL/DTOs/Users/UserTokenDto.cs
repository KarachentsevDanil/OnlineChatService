namespace OCS.BLL.DTOs.Users
{
    public class UserTokenDto
    {
        public GetUserDto User { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}