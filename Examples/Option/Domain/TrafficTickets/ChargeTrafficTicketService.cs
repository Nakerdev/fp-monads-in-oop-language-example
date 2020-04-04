using LanguageExt;

namespace Examples.Option.Domain.TrafficTickets
{
    public sealed class ChargeTrafficTicketService
    {
        Either<Error, TrafficTicket> Execute() 
        {
            return (TrafficTicket) null;
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