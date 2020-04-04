using LanguageExt;

namespace Examples.Option.Domain
{
    public interface UserRepository
    {
        void Create(User user);
        Option<User> SearchBy(string email);
    }
}