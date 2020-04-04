using LanguageExt;
using Examples.Option.Domain.Driver;

namespace Examples.Option.Domain.TrafficTickets
{
    public sealed class ChargeTrafficTicketService
    {
        private readonly DriverRepository driverRepository;
        private readonly TrafficTicketRepository trafficTicketRepository;

        public ChargeTrafficTicketService(
            DriverRepository driverRepository,
            TrafficTicketRepository trafficTicketRepository)
        {
            this.driverRepository = driverRepository;
            this.trafficTicketRepository = trafficTicketRepository;
        }

        Either<Error, TrafficTicket> Execute(TrafficTicketChargeRequest request)
        {
            return
                from driver in SearchDriverBy(request.DriverPersonalIdentificationCode)
                from trafficTicket in SearchTrafficTicketBy(request.TrafficTicketId)
                from chargeId in PayTrafficTicket(
                    driver: driver, 
                    trafficTicket: trafficTicket)
                from _ in MarkTrafficTicketAsPaid(
                    trafficTicket: trafficTicket, 
                    chargeId: chargeId)
                select trafficTicket;
        }

        Either<Error, Driver.Driver> SearchDriverBy(string personalIdentificationCode) 
        {
            return driverRepository
                .SearchBy(personalIdentificationCode: personalIdentificationCode)
                .ToEither(() => Error.DriverNotFound);
        }

        Either<Error, TrafficTicket> SearchTrafficTicketBy(string trafficTicketId)
        {
            return trafficTicketRepository
                .SearchBy(id: trafficTicketId)
                .ToEither(() => Error.TrafficTicketNotFound);
        }

        Either<Error, string> PayTrafficTicket(
            TrafficTicket trafficTicket,
            Driver.Driver driver)
        {
            //pay traffic ticket with stripe for example using driver object 
            //for extract driver information.
            return "chargeId";
        }

        Either<Error, Unit> MarkTrafficTicketAsPaid(
            TrafficTicket trafficTicket,
            string chargeId)
        {
            trafficTicket.MarkAsPaid(chargeId);
            return Prelude.unit;
        }
    }

    public sealed class TrafficTicketChargeRequest 
    {
        public string TrafficTicketId { get; }
        public string DriverPersonalIdentificationCode { get; }
    }

    public enum Error 
    {
        DriverNotFound,
        TrafficTicketNotFound
    }
}