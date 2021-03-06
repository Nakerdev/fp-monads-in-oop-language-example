﻿using Moq;
using NUnit.Framework;
using Examples.Domain.Driver;
using Examples.Domain.TrafficTickets;
using FluentAssertions;

namespace Tests.Domain.TrafficTickets
{
    [TestFixture]
    public class ChargeTrafficTicketsServiceTests
    {
        Mock<DriverRepository> driverRepository;
        Mock<TrafficTicketRepository> trafficTicketsRepository;
        ChargeTrafficTicketService service;

        [SetUp]
        public void SetUp() 
        {
            driverRepository = new Mock<DriverRepository>();
            trafficTicketsRepository = new Mock<TrafficTicketRepository>();
            service = new ChargeTrafficTicketService(
                driverRepository: driverRepository.Object,
                trafficTicketRepository: trafficTicketsRepository.Object);
        }

        internal sealed class UnsafeExecute : ChargeTrafficTicketsServiceTests
        {
            [Test]
            public void ChargesTrafficTickets()
            {
                var request = BuildRequest();
                driverRepository
                    .Setup(x => x.UnsafeSearchBy(request.DriverPersonalIdentificationCode))
                    .Returns(BuildDriver(request.DriverPersonalIdentificationCode));
                trafficTicketsRepository
                    .Setup(x => x.UnsafeSearchBy(request.TrafficTicketId))
                    .Returns(CreateUnpaidTrafficTicket(request.TrafficTicketId));

                var trafficTicket = service.UnsafeExecute(request);

                trafficTicket.IsPaid.Should().BeTrue();
                trafficTicketsRepository
                    .Verify(x => x.Update(It.Is<TrafficTicket>(y =>
                        y.IsPaid
                        && y.ChargeId.IsSome)), Times.Once);
            }

            [Test]
            public void DoesNotChargeTrafficTicketWhenDriverNotFound()
            {
                var request = BuildRequest();
                driverRepository
                    .Setup(x => x.UnsafeSearchBy(It.IsAny<string>()))
                    .Returns((Driver)null);

                System.Action result = () => service.UnsafeExecute(request);

                result.Should().Throw<DriverNotFoundException>();
            }

            public void DoesNotChargeTrafficTicketWhenTrafficTicketNotFound()
            {
                var request = BuildRequest();
                driverRepository
                    .Setup(x => x.UnsafeSearchBy(It.IsAny<string>()))
                    .Returns(BuildDriver());
                trafficTicketsRepository
                    .Setup(x => x.UnsafeSearchBy(It.IsAny<string>()))
                    .Returns((TrafficTicket) null);

                System.Action result = () => service.UnsafeExecute(request);

                result.Should().Throw<TrafficTicketNotFound>();
            }
        }

        internal sealed class SafeExecute : ChargeTrafficTicketsServiceTests
        {
            [Test]
            public void ChargesTrafficTickets()
            {
                var request = BuildRequest();
                driverRepository
                    .Setup(x => x.SafeSearchBy(request.DriverPersonalIdentificationCode))
                    .Returns(BuildDriver(request.DriverPersonalIdentificationCode));
                trafficTicketsRepository
                    .Setup(x => x.SafeSearchBy(request.TrafficTicketId))
                    .Returns(CreateUnpaidTrafficTicket(request.TrafficTicketId));

                var result = service.SafeExecute(request);

                result.IsRight.Should().BeTrue();
                result.IfRight(trafficTicket => trafficTicket.IsPaid.Should().BeTrue());
                trafficTicketsRepository
                    .Verify(x => x.Update(It.Is<TrafficTicket>(y =>
                        y.IsPaid
                        && y.ChargeId.IsSome)), Times.Once);
            }

            [Test]
            public void DoesNotChargeTrafficTicketWhenDriverNotFound()
            {
                var request = BuildRequest();
                driverRepository
                    .Setup(x => x.SafeSearchBy(It.IsAny<string>()))
                    .Returns((Driver)null);

                var result = service.SafeExecute(request);

                result.IsLeft.Should().BeTrue();
                result.IfLeft(error => error.Should().Be(Error.DriverNotFound));
            }

            [Test]
            public void DoesNotChargeTrafficTicketWhenTrafficTicketNotFound()
            {
                var request = BuildRequest();
                driverRepository
                    .Setup(x => x.SafeSearchBy(It.IsAny<string>()))
                    .Returns(BuildDriver());
                trafficTicketsRepository
                    .Setup(x => x.SafeSearchBy(It.IsAny<string>()))
                    .Returns((TrafficTicket)null);

                var result = service.SafeExecute(request);

                result.IsLeft.Should().BeTrue();
                result.IfLeft(error => error.Should().Be(Error.TrafficTicketNotFound));
            }
        }

        private TrafficTicketChargeRequest BuildRequest() 
        {
            return new TrafficTicketChargeRequest(
                trafficTicketId: "TT-0001",
                driverPersonalIdentificationCode: "00000000X");
        }

        private Driver BuildDriver(
            string personalIdentificationCode = "00000000X")
        {
            return new Driver(
                personalIdentificationCode: personalIdentificationCode,
                firstName: "Alvaro",
                lastName: "González",
                telephone: null);
        }

        private TrafficTicket CreateUnpaidTrafficTicket(
            string id = "TT-0000")
        {
            return TrafficTicket.CreateUnpaidTrafficTicket(
                id: id,
                ammount: 150,
                driverPersonalIndentificationCode: "00000000X");
        }
    }
}