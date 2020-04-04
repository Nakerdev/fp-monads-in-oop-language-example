using System.Collections.Generic;
using System.Linq;
using Examples.Option.Domain.User;
using LanguageExt;

namespace Examples.Option.Infraestructure.User
{
    public class StaticUserRepository : UserRepository
    {
        private List<UserPersistenceModel> users;

        public StaticUserRepository()
        {
            users = new List<UserPersistenceModel>
            {
                new UserPersistenceModel
                {
                    Email = "maria@email.com",
                    FirstName = "Maria",
                    LastName = "González",
                    Telephone = "652365269"
                },
                new UserPersistenceModel
                {
                    Email = "alvaro@email.com",
                    FirstName = "Alvaro",
                    LastName = "González",
                    Telephone = null
                }
            };
        }

        public void Create(Domain.User.User user)
        {
            var userState = user.State;
            var persistenceModel = BuildPersistenceModelFrom(userState);
            users.Add(persistenceModel);
        }

        public Option<Domain.User.User> SearchBy(string email)
        {
            var foundUser = users.FirstOrDefault(x => x.Email == email);
            if(foundUser == null) 
            {
                return Prelude.None;
            }
            return BuildUserFrom(foundUser);
        }

        private UserPersistenceModel BuildPersistenceModelFrom(
            Domain.User.User.PersistanceState persistanceState)
        {
            return new UserPersistenceModel
            {
                Email = persistanceState.Email,
                FirstName = persistanceState.FirstName,
                LastName = persistanceState.LastName,
                Telephone = persistanceState.Telephone
            };
        }

        private Domain.User.User BuildUserFrom(UserPersistenceModel persistenceModel)
        {
            return new Domain.User.User(
                email: persistenceModel.Email,
                firstName: persistenceModel.FirstName,
                lastName: persistenceModel.LastName,
                telephone: persistenceModel.Telephone);
        }
    }
}
