using LanguageExt;

namespace Examples.Option.Domain.Driver
{
    public sealed class Driver
    {
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Option<string> Telephone { get; }
        public PersistanceState State => new PersistanceState(
            email: Email,
            firstName: FirstName,
            lastName: LastName,
            telephone: Telephone.IfNoneUnsafe(() => null));

        public Driver(
            string email,
            string firstName,
            string lastName,
            Option<string> telephone)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Telephone = telephone;
        }

        public Driver(PersistanceState state)
        {
            Email = state.Email;
            FirstName = state.FirstName;
            LastName = state.LastName;
            Telephone = state.Telephone;
        }

        public sealed class PersistanceState
        {
            public string Email { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public string Telephone { get; }

            public PersistanceState(
                string email,
                string firstName,
                string lastName,
                string telephone)
            {
                Email = email;
                FirstName = firstName;
                LastName = lastName;
                Telephone = telephone;
            }
        }
    }
}