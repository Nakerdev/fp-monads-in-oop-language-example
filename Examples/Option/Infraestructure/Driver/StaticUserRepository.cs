using System.Collections.Generic;
using System.Linq;
using Examples.Option.Domain.Driver;
using LanguageExt;

namespace Examples.Option.Infraestructure.Driver
{
    public class StaticUserRepository : DriverRepository
    {
        private List<Driver> users;

        public StaticUserRepository()
        {
            users = new List<Driver>
            {
                new Driver
                {
                    Email = "maria@email.com",
                    FirstName = "Maria",
                    LastName = "González",
                    Telephone = "652365269"
                },
                new Driver
                {
                    Email = "alvaro@email.com",
                    FirstName = "Alvaro",
                    LastName = "González",
                    Telephone = null
                }
            };
        }

        public Option<Domain.Driver.Driver> SearchBy(string email)
        {
            var foundUser = users.FirstOrDefault(x => x.Email == email);
            if(foundUser == null) 
            {
                return Prelude.None;
            }
            return BuildUserFrom(foundUser);
        }

        private Driver BuildPersistenceModelFrom(
            Domain.Driver.Driver.PersistanceState persistanceState)
        {
            return new Driver
            {
                Email = persistanceState.Email,
                FirstName = persistanceState.FirstName,
                LastName = persistanceState.LastName,
                Telephone = persistanceState.Telephone
            };
        }

        private Domain.Driver.Driver BuildUserFrom(Driver persistenceModel)
        {
            return new Domain.Driver.Driver(
                email: persistenceModel.Email,
                firstName: persistenceModel.FirstName,
                lastName: persistenceModel.LastName,
                telephone: persistenceModel.Telephone);
        }
    }
}
