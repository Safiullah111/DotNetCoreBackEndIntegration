namespace API.Work.Application.Contract.Common;

public class PaginatedResponse<T> : ApiResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

    public PaginatedResponse(T data, int pageNumber, int pageSize, int totalRecords, string? message = null)
    {
        Success = true;
        Message = message;
        Payload = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
}
