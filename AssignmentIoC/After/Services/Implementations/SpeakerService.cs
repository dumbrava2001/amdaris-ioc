using AssignmentIoC.After.Exceptions;
using AssignmentIoC.After.Repositories;
using AssignmentIoC.Before;

namespace AssignmentIoC.After.Services.Implementations;

public class SpeakerService : ISpeakerService
{
    private const int NecessaryYearsOfExperience = 10;
    private const int NecessaryCertificationsCount = 3;
    private const int NecessaryApprovedSessionsOffset = 0;

    private readonly ISpeakerRepository _speakerRepository;
    private readonly IDomainService _domainService;
    private readonly IEmployerService _employerService;
    private readonly ISessionService _sessionService;
    private readonly IFeeService _feeService;

    public SpeakerService(ISpeakerRepository speakerRepository,
        IDomainService domainService, IEmployerService employerService,
        ISessionService sessionService, IFeeService feeService)
    {
        _speakerRepository = speakerRepository;
        _domainService = domainService;
        _employerService = employerService;
        _sessionService = sessionService;
        _feeService = feeService;
    }

    public Guid RegisterSpeaker(Speaker speaker)
    {
        CheckSpeakerDataIntegrity(speaker);
        CheckIfSpeakerMeetsGeneralRequirements(speaker);
        _sessionService.ApproveRegisteredSpeakerSession(speaker.Sessions);
        CheckSpeakerApprovedSessionRequirements(speaker.Sessions);

        speaker.RegistrationFee = _feeService.GetSpeakerRegistrationFee(speaker);

        // TODO to add using statement when used with dbContext ?
        var savedSpeaker = _speakerRepository.Save(speaker);

        return savedSpeaker.Id;
    }

    private void CheckSpeakerDataIntegrity(Speaker speaker)
    {
        if (string.IsNullOrWhiteSpace(speaker.FirstName))
        {
            throw new ArgumentNullException(speaker.FirstName, "First Name is required");
        }

        if (string.IsNullOrWhiteSpace(speaker.LastName))
        {
            throw new ArgumentNullException(speaker.LastName, "Last name is required.");
        }

        if (string.IsNullOrWhiteSpace(speaker.Email))
        {
            throw new ArgumentNullException(speaker.Email, "Email is required.");
        }
    }

    private void CheckIfSpeakerMeetsGeneralRequirements(Speaker speaker)
    {
        var generalRequirements = speaker.YearsOfExperience > NecessaryYearsOfExperience || speaker.HasBlog ||
                                  speaker.Certifications.Count > NecessaryCertificationsCount ||
                                  _employerService.Contains(speaker.Employer);

        var speakerEmailDomain = speaker.Email.Split("@").Last();
        var technicalRequirements = !_domainService.Contains(speakerEmailDomain) && speaker.Browser is not
            { Name: WebBrowser.BrowserName.InternetExplorer, MajorVersion: < 9 };

        if (!(generalRequirements && technicalRequirements))
        {
            throw new SpeakerDoesntMeetRequirementsException(
                "Speaker doesn't meet general requirements.");
        }

        if (speaker.Sessions.Count == 0)
        {
            throw new ArgumentException("Can't register speaker with no sessions.");
        }
    }

    private void CheckSpeakerApprovedSessionRequirements(IEnumerable<Session> sessionList)
    {
        var numberOfApprovedSessions = sessionList.Count(s => s.Approved);
        if (numberOfApprovedSessions <= NecessaryApprovedSessionsOffset)
        {
            throw new NoSessionsApprovedException("No sessions approved.");
        }
    }
}