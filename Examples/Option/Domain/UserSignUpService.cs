using System;
using LanguageExt;

namespace Examples.Option.Domain
{
    public class UserSignUpService
    {
        private readonly UserRepository userRepository;

        public UserSignUpService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
    }

    public sealed class UserSignUpRequest
    {
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Option<string> Telephone { get; }

        public UserSignUpRequest(
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
    }

    public enum Error 
    {
        UserAlreadyExist
    }
}