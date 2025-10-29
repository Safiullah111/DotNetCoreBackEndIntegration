

namespace API.Work.Application.Contract.Common.Expections.BusinessExceptions;

public class RoleAppServicesException : BusinessException
{
    public RoleAppServicesException(string code, string message) : base(code, message)
    {
    }
}
