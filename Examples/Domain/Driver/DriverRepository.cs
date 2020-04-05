using LanguageExt;

namespace Examples.Domain.Driver
{
    public interface DriverRepository
    {
        Option<Driver> SearchBy(string personalIdentificationCode);
    }
}