using System.Collections.Generic;
using System.Linq;
using Examples.Domain.TrafficTickets;
using LanguageExt;

namespace Examples.Infraestructure.TrafficTickets
{
    public class StaticTrafficTicketsRepository : TrafficTicketRepository
    {
        private List<TrafficTicket> trafficTickets;

        public StaticTrafficTicketsRepository()
        {
            trafficTickets = new List<TrafficTicket>
            {
                new TrafficTicket
                {
                    Id = "TT-0000",
                    Ammount = 150,
                    DriverPersonalIndentificationCode = "00000000X",
                    IsPaid = false,
                    ChargeId = null
                },
                new TrafficTicket
                {
                    Id = "TT-0001",
                    Ammount = 50,
                    DriverPersonalIndentificationCode = "00000001X",
                    IsPaid = true,
                    ChargeId = "TK_SD34hjVkj435jG9"
                }
            };
        }

        public Option<Domain.TrafficTickets.TrafficTicket> SafeSearchBy(string id)
        {
            var foundUser = trafficTickets
                .FirstOrDefault(x => x.Id == id);
            if(foundUser == null) 
            {
                return Prelude.None;
            }
            return BuildTrafficTicketFrom(foundUser);
        }

        public Domain.TrafficTickets.TrafficTicket UnsafeSearchBy(string id)
        {
            var foundUser = trafficTickets
                .FirstOrDefault(x => x.Id == id);
            if (foundUser == null)
            {
                return null;
            }
            return BuildTrafficTicketFrom(foundUser);
        }

        public void Update(Domain.TrafficTickets.TrafficTicket trafficTicket)
        {
            var foundUser = trafficTickets
                .FirstOrDefault(x => x.Id == trafficTicket.Id);
            if (foundUser == null)
            {
                throw new TrafficTicketNotFound();
            }
            var state = trafficTicket.State;
            foundUser.IsPaid = state.IsPaid;
            foundUser.ChargeId = state.ChargeId;
        }

        private Domain.TrafficTickets.TrafficTicket BuildTrafficTicketFrom(TrafficTicket trafficTicket)
        {
            return new Domain.TrafficTickets.TrafficTicket(
                new Domain.TrafficTickets.TrafficTicket.PersistenceState(
                    id: trafficTicket.Id,
                    ammount: trafficTicket.Ammount,
                    driverPersonalIndentificationCode: trafficTicket.DriverPersonalIndentificationCode,
                    isPaid: trafficTicket.IsPaid,
                    chargeId: trafficTicket.ChargeId));
        }
    }
}