using LanguageExt;

namespace Examples.Domain.TrafficTickets
{
    public interface TrafficTicketRepository
    {
        Option<TrafficTicket> SafeSearchBy(string id);
        TrafficTicket UsafeSearchBy(string id);
        void Update(TrafficTicket trafficTicket);
    }
}
