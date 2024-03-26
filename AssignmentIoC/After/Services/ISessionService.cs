using AssignmentIoC.Before;

namespace AssignmentIoC.After.Services;

public interface ISessionService
{
    void ApproveRegisteredSpeakerSession(List<Session> sessions);
}