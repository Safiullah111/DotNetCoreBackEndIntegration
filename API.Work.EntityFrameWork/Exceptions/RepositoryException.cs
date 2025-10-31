
namespace API.Work.EntityFrameWork.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class GetByIdAsyncException : RepositoryException
{
    public GetByIdAsyncException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
public class GetAllAsyncException : RepositoryException
{
    public GetAllAsyncException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
public class AddAsyncException : RepositoryException
{
    public AddAsyncException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
public class UpdateAsyncException : RepositoryException
{
    public UpdateAsyncException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
public class DeleteAsyncException : RepositoryException
{
    public DeleteAsyncException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
