using AssignmentIoC.After.Exceptions;
using AssignmentIoC.After.Repositories;
using AssignmentIoC.Before;

namespace AssignmentIoC.After.Services.Implementations;

public class SpeakerService : ISpeakerService
{
    private readonly ISpeakerRepository _speakerRepository;
    private readonly int _necessaryYearsOfExperience;
    private readonly int _necessaryCertificationsCount;
    private readonly IDomainService _domainService;
    private readonly IEmployerService _employerService;

    public SpeakerService(ISpeakerRepository speakerRepository, int necessaryCertificationsCount,
        int necessaryYearsOfExperience, IDomainService domainService, IEmployerService employerService)
    {
        _speakerRepository = speakerRepository;
        _necessaryCertificationsCount = necessaryCertificationsCount;
        _necessaryYearsOfExperience = necessaryYearsOfExperience;
        _domainService = domainService;
        _employerService = employerService;
    }

    /// <summary>
    /// Register a speaker
    /// </summary>
    /// <returns>speakerID</returns>
    public int RegisterSpeaker(Speaker speaker)
    {
        //lets init some vars
        int? speakerId = null;
        bool appr = false;
        var ot = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };

        CheckSpeakerDataIntegrity(speaker);
        CheckIfSpeakerMeetsGeneralRequirements(speaker);
        
        foreach (var session in Sessions)
        {
            foreach (var tech in ot)
            {
                if (session.Title.Contains(tech) || session.Description.Contains(tech))
                {
                    session.Approved = false;
                    break;
                }

                session.Approved = true;
                appr = true;
            }
        }

        if (appr)
        {
            //if we got this far, the speaker is approved
            //let's go ahead and register him/her now.
            //First, let's calculate the registration fee. 
            //More experienced speakers pay a lower fee.
            if (YearsOfExperience <= 1)
            {
                RegistrationFee = 500;
            }
            else if (YearsOfExperience >= 2 && YearsOfExperience <= 3)
            {
                RegistrationFee = 250;
            }
            else if (YearsOfExperience >= 4 && YearsOfExperience <= 5)
            {
                RegistrationFee = 100;
            }
            else if (YearsOfExperience >= 6 && YearsOfExperience <= 9)
            {
                RegistrationFee = 50;
            }
            else
            {
                RegistrationFee = 0;
            }


            //Now, save the speaker and sessions to the db.
            try
            {
                speakerId = repository.SaveSpeaker(this);
            }
            catch (Exception e)
            {
                //in case the db call fails 
            }
        }
        else
        {
            throw new NoSessionsApprovedException("No sessions approved.");
        }


        //if we got this far, the speaker is registered.
        return speakerId;
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
        var generalRequirements = speaker.YearsOfExperience > _necessaryYearsOfExperience || speaker.HasBlog ||
                                  speaker.Certifications.Count > _necessaryCertificationsCount ||
                                  _employerService.Contains(speaker.Employer);

        var speakerEmailDomain = speaker.Email.Split("@").Last();
        var technicalRequirements = !_domainService.Contains(speakerEmailDomain) && speaker.Browser is not
            { Name: WebBrowser.BrowserName.InternetExplorer, MajorVersion: < 9 };

        if (!(generalRequirements && technicalRequirements))
        {
            throw new SpeakerDoesntMeetRequirementsException(
                "Speaker doesn't meet our abitrary and capricious standards.");
        }

        if (speaker.Sessions.Count == 0)
        {
            throw new ArgumentException("Can't register speaker with no sessions to present.");
        }
    }
}