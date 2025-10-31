

namespace API.Work.Application.Contract.Common;

public  class PaginationDto
{
    public string Sorting { get; set; } = "FirstName";
    public int MaxResultCount { get; set; } = 10;
    public int SkipCount { get; set; } = 0;
}
