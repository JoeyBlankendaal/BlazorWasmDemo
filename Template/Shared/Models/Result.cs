namespace Template.Shared.Models;

public class Result
{
    public string Message => Messages != null ? string.Join(", ", Messages) : string.Empty;

    public bool HasSucceeded { get; set; }
    public string[] Messages { get; set; }
}
