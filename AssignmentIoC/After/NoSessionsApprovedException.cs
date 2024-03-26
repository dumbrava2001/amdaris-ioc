namespace AssignmentIoC.After;

public class NoSessionsApprovedException : Exception
{
    public NoSessionsApprovedException(string message)
        : base(message)
    {
    }
}