namespace AssignmentIoC.After.Repositories.Implementations;

public class DomainListRepository : IDomainRepository
{
    private readonly List<string> _domainList = new() { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };

    public void Add(string domain)
    {
        _domainList.Add(domain);
    }

    public bool Contains(string domainToFind)
    {
        return _domainList.Contains(domainToFind);
    }

    public IEnumerable<string> GetAll()
    {
        return new List<string>(_domainList);
    }

    public void Delete(string domain)
    {
        _domainList.Remove(domain);
    }
}