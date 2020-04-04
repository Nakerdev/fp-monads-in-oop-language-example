using LanguageExt;
using Examples.Option.Domain.Driver;

namespace Examples.Option.Domain.TrafficTickets
{
    public sealed class ChargeTrafficTicketService
    {
        private readonly DriverRepository driverRepository;

        public ChargeTrafficTicketService(DriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        Either<Error, TrafficTicket> Execute() 
        {
            return null;
        }
    }

    public sealed class TrafficTicketChargeRequest 
    {
        public string TrafficTicketId { get; }
        public string UserPersonalIdentificationCode { get; }
    }

    public enum Error 
    {
        UserNotFound 
    }
}