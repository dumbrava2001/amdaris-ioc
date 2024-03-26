using AssignmentIoC.Before;

namespace AssignmentIoC.After;

public class Speaker
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int? YearsOfExperience { get; set; }
    public bool HasBlog { get; set; }
    
    public string BlogURL { get; set; }
    public WebBrowser Browser { get; set; }
    public List<string> Certifications { get; set; }
    public string Employer { get; set; }
    public double RegistrationFee { get; set; }
    public List<Session> Sessions { get; set; }
}