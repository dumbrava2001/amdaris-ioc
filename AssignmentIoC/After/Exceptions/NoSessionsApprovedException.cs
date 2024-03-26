namespace AssignmentIoC.After.Exceptions;

public class NoSessionsApprovedException : Exception
{
    public NoSessionsApprovedException(string message)
        : base(message)
    {
    }
}