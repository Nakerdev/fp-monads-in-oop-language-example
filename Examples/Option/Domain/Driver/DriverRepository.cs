using LanguageExt;

namespace Examples.Option.Domain.Driver
{
    public interface DriverRepository
    {
        Option<Driver> SearchBy(string email);
    }
}