using LanguageExt;

namespace Examples.Option.Domain
{
    public sealed class User
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

        public User(
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

        public User(PersistanceState state)
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