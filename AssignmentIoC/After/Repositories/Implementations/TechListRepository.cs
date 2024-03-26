namespace AssignmentIoC.After.Repositories.Implementations;

public class TechListRepository : ITechRepository
{
    private readonly List<string> _techList = new() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
    public IEnumerable<string> GetAll()
    {
        return new List<string>(_techList);
    }

    public void Add(string tech)
    {
        _techList.Add(tech);
    }
}