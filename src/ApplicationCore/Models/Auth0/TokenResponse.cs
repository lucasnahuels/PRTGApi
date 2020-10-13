namespace ApplicationCore.Models.Auth0
{
    public class TokenResponse
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public long Expires_in { get; set; }
    }
}
