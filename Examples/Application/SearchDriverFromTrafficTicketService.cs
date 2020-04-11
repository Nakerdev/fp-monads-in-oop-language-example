using LanguageExt;

namespace Examples.Application
{
    public sealed class SearchDriverFromTrafficTicketService
    {
        private readonly TrafficTicketRepository trafficTicketRepository;
        private readonly DriverRepository driverRepository;

        public SearchDriverFromTrafficTicketService(
            TrafficTicketRepository trafficTicket, 
            DriverRepository driverRepository)
        {
            this.trafficTicketRepository = trafficTicket;
            this.driverRepository = driverRepository;
        }

        public string UnsafeGetFullName(int trafficTicketId) 
        {
            var trafficTicket = trafficTicketRepository.UnsafeSearchBy(trafficTicketId);
            if(trafficTicket == null) 
            {
                return null;
            }
            //Driver can be null!!
            var driver = driverRepository.UnsafeSearchBy(trafficTicket.DriverPersonalIdentificationCode);
            //Possible NullPointerException!!
            return $"{driver.FirstName} {driver.LastName}";
        }
    }

    public interface TrafficTicketRepository
    {
        Option<TrafficTicket> SafeSearchBy(int trafficTicketId);
        TrafficTicket UnsafeSearchBy(int trafficTicketId);
    }

    public sealed class TrafficTicket 
    {
        public string DriverPersonalIdentificationCode { get; }

        public TrafficTicket(string driverPersonalIdentificationCode)
        {
            DriverPersonalIdentificationCode = driverPersonalIdentificationCode;
        }
    }

    public interface DriverRepository 
    {
        Option<Driver> SearchBy(string personalIdentificationCode);
        Driver UnsafeSearchBy(string personalIdentificationCode);
    }

    public sealed class Driver
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Driver(
            string firstName,
            string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
