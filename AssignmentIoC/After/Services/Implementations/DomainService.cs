using AssignmentIoC.After.Repositories;

namespace AssignmentIoC.After.Services.Implementations;

public class DomainService : IDomainService
{
    private readonly IDomainRepository _domainRepository;

    public DomainService(IDomainRepository domainRepository)
    {
        _domainRepository = domainRepository;
    }

    public bool Contains(string domain)
    {
        return _domainRepository.Contains(domain);
    }
}