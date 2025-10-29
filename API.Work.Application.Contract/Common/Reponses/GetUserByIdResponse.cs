namespace API.Work.Application.Contract.Common.Reponses;

public class GetUserByIdResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
