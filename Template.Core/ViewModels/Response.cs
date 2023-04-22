namespace Template.Core.ViewModels;

public class Response<T>
{
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public T? Data { get; set; }
    public bool Success { get; set; }
}
