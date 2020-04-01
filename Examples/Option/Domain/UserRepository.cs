using Examples.Option.Domain;
using LanguageExt;

namespace Examples.Option
{
    public interface UserRepository
    {
        void Create(User user);
        Option<User> SearchBy(string email);
    }
}