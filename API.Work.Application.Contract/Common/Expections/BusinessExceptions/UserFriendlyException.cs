namespace API.Work.Application.Contract.Common.Expections.BusinessExceptions;

public class UserFriendlyException : BusinessException
{
    public UserFriendlyException(string code, string message) : base(code, message)
    {
    }
}
