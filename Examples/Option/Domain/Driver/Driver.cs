using LanguageExt;

namespace Examples.Option.Domain.Driver
{
    public sealed class Driver
    {
        public string PersonalIdentificationCode { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Option<string> Telephone { get; }
        public PersistanceState State => new PersistanceState(
            personalIdentificationCode: PersonalIdentificationCode,
            firstName: FirstName,
            lastName: LastName,
            telephone: Telephone.IfNoneUnsafe(() => null));

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

        public Driver(PersistanceState state)
        {
            PersonalIdentificationCode = state.PersonalIdentificationCode;
            FirstName = state.FirstName;
            LastName = state.LastName;
            Telephone = state.Telephone;
        }

        public sealed class PersistanceState
        {
            public string PersonalIdentificationCode { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public string Telephone { get; }

            public PersistanceState(
                string personalIdentificationCode,
                string firstName,
                string lastName,
                string telephone)
            {
                PersonalIdentificationCode = personalIdentificationCode;
                FirstName = firstName;
                LastName = lastName;
                Telephone = telephone;
            }
        }
    }
}