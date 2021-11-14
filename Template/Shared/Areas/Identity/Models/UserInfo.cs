namespace Template.Shared.Areas.Identity.Models;

public class UserInfo
{
    public User CurrentUser { get; set; }
    public Dictionary<string, string> ExposedClaims { get; set; }
    public bool IsAuthenticated { get; set; }
}
