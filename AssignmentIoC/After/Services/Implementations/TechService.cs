using AssignmentIoC.After.Repositories;

namespace AssignmentIoC.After.Services.Implementations;

public class TechService : ITechService
{
    private readonly ITechRepository _techRepository;

    public TechService(ITechRepository techRepository)
    {
        _techRepository = techRepository;
    }

    public IEnumerable<string> GetAll()
    {
        return _techRepository.GetAll();
    }
}