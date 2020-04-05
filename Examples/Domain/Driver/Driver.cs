using LanguageExt;

namespace Examples.Domain.Driver
{
    public sealed class Driver
    {
        public string PersonalIdentificationCode { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Option<string> Telephone { get; }

        public Driver(
            string personalIdentificationCode,
            string firstName,
            string lastName,
            Option<string> telephone)
        {
            PersonalIdentificationCode = personalIdentificationCode;
            FirstName = firstName;
            LastName = lastName;
            Telephone = telephone;
        }
    }
}