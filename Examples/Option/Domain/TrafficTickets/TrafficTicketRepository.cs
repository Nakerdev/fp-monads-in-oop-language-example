using LanguageExt;

namespace Examples.Option.Domain.TrafficTickets
{
    public interface TrafficTicketRepository
    {
        Option<TrafficTicket> SearchBy(string id);
        void Update(TrafficTicket trafficTicket);
    }
}
