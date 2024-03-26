using AssignmentIoC.After.Repositories;

namespace AssignmentIoC.After.Services.Implementations;

public class FeeService : IFeeService
{
    private readonly IRegistrationFeeRepository _registrationFeeRepository;

    public FeeService(IRegistrationFeeRepository registrationFeeRepository)
    {
        _registrationFeeRepository = registrationFeeRepository;
    }

    public double GetSpeakerRegistrationFee(Speaker speaker)
    {
        var registrationFee = _registrationFeeRepository.FindByCriteria(fee =>
            speaker.YearsOfExperience >= fee.StartOffset && speaker.YearsOfExperience <= fee.EndOffset)
            .FirstOrDefault();

        return registrationFee!.Fee;
    }
}