using System;
using LanguageExt;

namespace Examples.Domain.TrafficTickets
{
    public interface TrafficTicketRepository
    {
        Option<TrafficTicket> SafeSearchBy(string id);
        TrafficTicket UnsafeSearchBy(string id);
        void Update(TrafficTicket trafficTicket);
    }

    public class TrafficTicketNotFound : Exception { }
}
