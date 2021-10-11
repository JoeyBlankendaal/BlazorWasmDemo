namespace Template.Shared.Models;

public class UserInfo
{
    public User CurrentUser { get; set; }
    public Dictionary<string, string> ExposedClaims { get; set; }
    public bool IsAuthenticated { get; set; }
}
