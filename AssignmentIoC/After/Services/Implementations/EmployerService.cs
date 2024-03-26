using AssignmentIoC.After.Repositories;

namespace AssignmentIoC.After.Services.Implementations;

public class EmployerService : IEmployerService
{
    private readonly IEmployerRepository _employerRepository;

    public EmployerService(IEmployerRepository employerRepository)
    {
        _employerRepository = employerRepository;
    }

    public bool Contains(string employer)
    {
        return _employerRepository.Contains(employer);
    }
}