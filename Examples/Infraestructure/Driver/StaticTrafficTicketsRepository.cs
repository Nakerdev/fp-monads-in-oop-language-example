using System.Collections.Generic;
using System.Linq;
using Examples.Domain.Driver;
using LanguageExt;

namespace Examples.Infraestructure.Driver
{
    public sealed class StaticTrafficTicketsRepository : DriverRepository
    {
        private List<Driver> users;

        public StaticTrafficTicketsRepository()
        {
            users = new List<Driver>
            {
                new Driver
                {
                    PersonalIdentificationCode = "00000000X",
                    FirstName = "Maria",
                    LastName = "González",
                    Telephone = "652365269"
                },
                new Driver
                {
                    PersonalIdentificationCode = "00000001X",
                    FirstName = "Alvaro",
                    LastName = "González",
                    Telephone = null
                }
            };
        }

        public Option<Domain.Driver.Driver> SafeSearchBy(string personalIdentificationCode)
        {
            var foundUser = users
                .FirstOrDefault(x => x.PersonalIdentificationCode == personalIdentificationCode);
            if(foundUser == null) 
            {
                return Prelude.None;
            }
            return BuildUserFrom(foundUser);
        }

        public Domain.Driver.Driver UnsafeSearchBy(string personalIdentificationCode)
        {
            var foundUser = users
                .FirstOrDefault(x => x.PersonalIdentificationCode == personalIdentificationCode);
            if (foundUser == null)
            {
                return null;
            }
            return BuildUserFrom(foundUser);
        }

        private Domain.Driver.Driver BuildUserFrom(Driver persistenceModel)
        {
            return new Domain.Driver.Driver(
                personalIdentificationCode: persistenceModel.PersonalIdentificationCode,
                firstName: persistenceModel.FirstName,
                lastName: persistenceModel.LastName,
                telephone: persistenceModel.Telephone);
        }
    }
}
