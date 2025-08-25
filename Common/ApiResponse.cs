public class ApiResponse
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
