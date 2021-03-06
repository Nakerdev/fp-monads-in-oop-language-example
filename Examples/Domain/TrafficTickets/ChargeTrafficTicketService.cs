﻿using LanguageExt;
using Examples.Domain.Driver;
using System;

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

        public TrafficTicket UnsafeExecute(TrafficTicketChargeRequest request)
        {
            var driver = driverRepository.UnsafeSearchBy(request.DriverPersonalIdentificationCode);
            if(driver == null) 
            {
                throw new DriverNotFoundException(); 
            }

            var trafficTicket = trafficTicketRepository.UnsafeSearchBy(request.TrafficTicketId);
            if (trafficTicket == null)
            {
                throw new TrafficTicketNotFoundException();
            }

            //var paymentRequest = new PaymentRequest(...)
            //var chargeId = paymentService.pay(paymentRequest)
            var chargeId = "chargeId";

            trafficTicket.MarkAsPaid(chargeId);
            trafficTicketRepository.Update(trafficTicket);
            return trafficTicket;
        }


        public Either<Error, TrafficTicket> SafeExecute(TrafficTicketChargeRequest request)
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
                    .SafeSearchBy(personalIdentificationCode)
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
                trafficTicketRepository.Update(trafficTicket);
                return Prelude.unit;
            }
        }
    }

    public sealed class TrafficTicketChargeRequest 
    {
        public string TrafficTicketId { get; }
        public string DriverPersonalIdentificationCode { get; }

        public TrafficTicketChargeRequest(
            string trafficTicketId, 
            string driverPersonalIdentificationCode)
        {
            TrafficTicketId = trafficTicketId;
            DriverPersonalIdentificationCode = driverPersonalIdentificationCode;
        }
    }

    public enum Error 
    {
        DriverNotFound,
        TrafficTicketNotFound
    }

    public sealed class DriverNotFoundException : Exception { }
    public sealed class TrafficTicketNotFoundException : Exception { }
}