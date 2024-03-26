namespace AssignmentIoC.After.Repositories.Implementations;

public class EmployerListRepository : IEmployerRepository
{
    private readonly List<string> _employerList = new() { "Microsoft", "Google", "Fog Creek Software", "37Signals" };


    public void Add(string employer)
    {
        _employerList.Add(employer);
    }

    public void Delete(string employer)
    {
        _employerList.Remove(employer);
    }

    public IEnumerable<string> GetAll()
    {
        return new List<string>(_employerList);
    }

    public bool Contains(string employer)
    {
        return _employerList.Contains(employer);
    }
}