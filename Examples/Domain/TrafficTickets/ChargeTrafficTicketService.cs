﻿using LanguageExt;
using Examples.Option.Domain.Driver;

namespace Examples.Domain.TrafficTickets
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

        Either<Error, TrafficTicket> UnsafeExecute(TrafficTicketChargeRequest request)
        {
            var driver = driverRepository.SearchBy(personalIdentificationCode: request.DriverPersonalIdentificationCode);
            if(driver == null) 
            {
                return Error.DriverNotFound; 
            }

            //Ups.... trafficTicket can be null!!!
            var trafficTicket = trafficTicketRepository.UsafeSearchBy(request.TrafficTicketId);

            //var paymentRequest = new PaymentRequest(...)
            //var chargeId = paymentService.pay(paymentRequest)
            var chargeId = "chargeId";

            //Possible NullPointerException!!!
            trafficTicket.MarkAsPaid(chargeId);

            return trafficTicket;
        }


        Either<Error, TrafficTicket> SafeExecute(TrafficTicketChargeRequest request)
        {
            return
                from driver in SearchDriverBy(request.DriverPersonalIdentificationCode)
                from trafficTicket in SearchTrafficTicketBy(request.TrafficTicketId)
                from chargeId in PayTrafficTicket(trafficTicket, driver)
                from _ in MarkTrafficTicketAsPaid(trafficTicket, chargeId)
                select trafficTicket;

            Either<Error, Driver.Driver> SearchDriverBy(string personalIdentificationCode)
            {
                return driverRepository
                    .SearchBy(personalIdentificationCode: personalIdentificationCode)
                    .ToEither(() => Error.DriverNotFound);
            }

            Either<Error, TrafficTicket> SearchTrafficTicketBy(string trafficTicketId)
            {
                return trafficTicketRepository
                    .SafeSearchBy(id: trafficTicketId)
                    .ToEither(() => Error.TrafficTicketNotFound);
            }

            Either<Error, string> PayTrafficTicket(
                TrafficTicket trafficTicket,
                Driver.Driver driver)
            {
                //var paymentRequest = new PaymentRequest(...)
                //var chargeId = paymentService.pay(paymentRequest)
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