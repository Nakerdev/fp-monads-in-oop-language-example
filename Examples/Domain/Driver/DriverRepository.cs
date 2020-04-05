using LanguageExt;

namespace Examples.Domain.Driver
{
    public interface DriverRepository
    {
        Option<Driver> SafeSearchBy(string personalIdentificationCode);
        Driver UnsafeSearchBy(string personalIdentificationCode);
    }
}