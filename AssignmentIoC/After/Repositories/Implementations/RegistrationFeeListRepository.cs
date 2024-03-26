using AssignmentIoC.After.Models;

namespace AssignmentIoC.After.Repositories.Implementations;

public class RegistrationFeeListRepository : IRegistrationFeeRepository
{
    private readonly List<RegistrationFee> _registrationFees = new()
    {
        new RegistrationFee(id: Guid.NewGuid(), fee: 250, startOffset: 2, endOffset: 3),
        new RegistrationFee(id: Guid.NewGuid(), fee: 100, startOffset: 4, endOffset: 5),
        new RegistrationFee(id: Guid.NewGuid(), fee: 50, startOffset: 6, endOffset: 9),
        new RegistrationFee(id: Guid.NewGuid(), fee: 0, startOffset: 10, endOffset: 100)
    };

    public IEnumerable<RegistrationFee> FindByCriteria(Func<RegistrationFee, bool> searchFn)
    {
        return _registrationFees.Where(searchFn);
    }
}