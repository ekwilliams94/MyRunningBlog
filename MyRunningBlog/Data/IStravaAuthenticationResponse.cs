namespace MyRunningBlog.Data
{
    public interface IStravaAuthenticationResponse
    {
        string AccessToken { get; }
        string RefreshToken { get; }
        string TokenType { get; }
    }
}