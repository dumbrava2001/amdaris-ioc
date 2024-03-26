namespace AssignmentIoC.After.Repositories;

public interface ITechRepository
{
    IEnumerable<string> GetAll();
    void Add(string tech);
}