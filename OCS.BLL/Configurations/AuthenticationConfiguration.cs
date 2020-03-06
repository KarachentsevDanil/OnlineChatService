namespace OCS.BLL.Configurations
{
    public class AuthenticationConfiguration
    {
        public string TokenKey { get; set; }

        public int TokenExpirationPeriodInDay { get; set; }
    }
}