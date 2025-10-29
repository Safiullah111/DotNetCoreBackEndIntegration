using API.Work.Application.Contract.Common.Expections.BusinessExceptions;

namespace API.Work.Application.Contract.Common.Expections;

public class UserNotFoundException : BusinessException
{
    public UserNotFoundException(string message, string code)
        : base(message, code)
    {
    }
}

public class InvalidCredentialsException : BusinessException
{
    public InvalidCredentialsException(string message, string code)
        : base(message, code)
    {
    }
}

public class UserInactiveException : BusinessException
{
    public UserInactiveException(string message, string code)
        : base(message, code)
    {
    }
}

public class AccountLockedException : BusinessException
{
    public AccountLockedException(string message, string code)
        : base(message, code)
    {
    }
}
public class RefershTokenException : BusinessException
{
    public RefershTokenException(string message, string code)
        : base(message, code)
    {
    }
}
