namespace API.Work.Application.Contract.Common.Expections.BusinessExceptions;

public abstract class BusinessException : Exception
{
    public string Code { get; }

    protected BusinessException(string message, string code) : base(message)
    {
        Code = code;
    }
}
