namespace AssignmentIoC.After.Repositories.Implementations;

public class SpeakerListRepository : ISpeakerRepository
{
    private readonly List<Speaker> _speakers = new();
    
    public Speaker Save(Speaker speaker)
    {
        speaker.Id = Guid.NewGuid();
        _speakers.Add(speaker);

        return speaker;
    }
}