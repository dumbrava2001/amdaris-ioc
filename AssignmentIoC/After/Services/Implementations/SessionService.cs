using AssignmentIoC.After.Repositories;
using AssignmentIoC.Before;

namespace AssignmentIoC.After.Services.Implementations;

public class SessionService : ISessionService
{
    private readonly ITechRepository _techRepository;

    public SessionService(ITechRepository techRepository)
    {
        _techRepository = techRepository;
    }

    public void ApproveRegisteredSpeakerSession(List<Session> sessions)
    {
        var techList = _techRepository.GetAll().ToList();
        foreach (var session in sessions)
        {
            session.Approved = true;
            var techsThatAreContainedInSession =
                techList.Where(tech => session.Title.Contains(tech) || session.Description.Contains(tech));
            
            foreach (var unused in techsThatAreContainedInSession)
            {
                session.Approved = false;
            }
        }
    }
}