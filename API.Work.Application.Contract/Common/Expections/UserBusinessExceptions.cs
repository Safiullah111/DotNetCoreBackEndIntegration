
using API.Work.Application.Contract.Common.Expections.BusinessExceptions;

namespace API.Work.Application.Contract.Common.Expections;


public class UserException : BusinessException
{
    public UserException(string message, string code) : base(message, code)
    {
    }
}