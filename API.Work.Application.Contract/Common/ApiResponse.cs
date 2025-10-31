using API.Work.Application.Contract.Enums;
using API.Work.Application.Contract.Localization;
using System.Text.Json.Serialization;

namespace API.Work.Application.Contract.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }
    public T Payload { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ApiError? Error { get; set; }

    // Convenience static factory methods:
    public static ApiResponse<T> Ok(T data, string? message = "Success")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Payload = data,
            Message = L.Get(key:message, data.ToString())
        };
    }

    public static ApiResponse<T> Fail(ApiError? error = null, string? message = "Validation failed")
    {
        return new ApiResponse<T> { Success = false, Message = message, Error = error };
    }

}

public class ApiError
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Code { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Entity { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Details { get; set; }
}