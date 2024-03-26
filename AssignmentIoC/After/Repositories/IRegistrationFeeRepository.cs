using AssignmentIoC.After.Models;

namespace AssignmentIoC.After.Repositories;

public interface IRegistrationFeeRepository
{
    IEnumerable<RegistrationFee> FindByCriteria(Func<RegistrationFee, bool> searchFn);
}