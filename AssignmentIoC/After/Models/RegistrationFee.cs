namespace AssignmentIoC.After.Models;

public class RegistrationFee
{
    public Guid Id { get; set; }
    public double Fee { get; set; }
    public double StartOffset { get; set; }
    public double EndOffset { get; set; }

    public RegistrationFee(Guid id, double fee, double startOffset, double endOffset)
    {
        Fee = fee;
        StartOffset = startOffset;
        EndOffset = endOffset;
    }
}