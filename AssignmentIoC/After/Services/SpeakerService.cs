using AssignmentIoC.After.Repositories;
using AssignmentIoC.Before;

namespace AssignmentIoC.After.Services;

public class SpeakerService : ISpeakerService
{
    private readonly ISpeakerRepository _speakerRepository;
    private readonly int _necessaryYearsOfExperience;
    private readonly int _necessaryCertificationsCount;

    public SpeakerService(ISpeakerRepository speakerRepository, int necessaryCertificationsCount,
        int necessaryYearsOfExperience)
    {
        _speakerRepository = speakerRepository;
        _necessaryCertificationsCount = necessaryCertificationsCount;
        _necessaryYearsOfExperience = necessaryYearsOfExperience;
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

        //DEFECT #5274 DA 12/10/2012
        //We weren't filtering out the prodigy domain so I added it.
        var domains = new List<string>() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };
        CheckSpeakerDataIntegrity(speaker);

        //put list of employers in array
        var emps = new List<string>() { "Microsoft", "Google", "Fog Creek Software", "37Signals" };

        //DFCT #838 Jimmy 
        var good = speaker.YearsOfExperience > _necessaryYearsOfExperience || speaker.HasBlog ||
                   speaker.Certifications.Count > _necessaryCertificationsCount || emps.Contains(speaker.Employer);


        if (!good)
        {
            //need to get just the domain from the email
            string emailDomain = Email.Split('@').Last();

            if (!domains.Contains(emailDomain) &&
                !(Browser.Name == WebBrowser.BrowserName.InternetExplorer &&
                  Browser.MajorVersion < 9))
            {
                good = true;
            }
        }

        if (good)
        {
            //DEFECT #5013 CO 1/12/2012
            //We weren't requiring at least one session
            if (Sessions.Count() != 0)
            {
                foreach (var session in Sessions)
                {
                    //foreach (var tech in nt)
                    //{
                    //    if (session.Title.Contains(tech))
                    //    {
                    //        session.Approved = true;
                    //        break;
                    //    }
                    //}

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
            }
            else
            {
                throw new ArgumentException("Can't register speaker with no sessions to present.");
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
                throw new Before.Speaker.NoSessionsApprovedException("No sessions approved.");
            }
        }
        else
        {
            throw new Before.Speaker.SpeakerDoesntMeetRequirementsException(
                "Speaker doesn't meet our abitrary and capricious standards.");
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
}