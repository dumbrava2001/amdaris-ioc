namespace AssignmentIoC.After.Repositories;

public interface IEmployerRepository
{
    void Add(string employer);
    void Delete(string employer);
    IEnumerable<string> GetAll();
    bool Contains(string employer);
}