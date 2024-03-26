namespace AssignmentIoC.After.Repositories;

public interface IDomainRepository
{
    void Add(string domain);
    bool Contains(string domainToFind);
    IEnumerable<string> GetAll();

    void Delete(string domain);
}