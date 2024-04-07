namespace Clean.Application.Models;

public class ApiResponseModel<TData>
{
    public bool IsSuccess { set; get; }
    public string Message { set; get; }
    public List<string> DetailMessages { get; set; }
    public TData Data { set; get; }
    public string ResultCode { get; internal set; }
}