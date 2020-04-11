using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.Application
{
    [TestFixture]
    public sealed class SearchDriverFromTrafficTicketServiceTests
    {
        private Mock<Examples.Application.TrafficTicketRepository> trafficTicketRepository;
        private Mock<Examples.Application.DriverRepository> driverRepository;
        private Examples.Application.SearchDriverFromTrafficTicketService service;

        [SetUp]
        public void SetUp() 
        {
            trafficTicketRepository = new Mock<Examples.Application.TrafficTicketRepository>();
            driverRepository = new Mock<Examples.Application.DriverRepository>();
            service = new Examples.Application.SearchDriverFromTrafficTicketService(
                trafficTicket: trafficTicketRepository.Object,
                driverRepository: driverRepository.Object);
        }

        [Test]
        public void GetsDriveFullName() 
        {
            int trafficTicketId = 1;
            var trafficTicket = new Examples.Application.TrafficTicket(
                driverPersonalIdentificationCode: "422345456P");
            trafficTicketRepository
                .Setup(x => x.UnsafeSearchBy(trafficTicketId))
                .Returns(trafficTicket);
            var driver = new Examples.Application.Driver(
                firstName: "Alvaro",
                lastName: "Gonzalez");
            driverRepository
                .Setup(x => x.UnsafeSearchBy(trafficTicket.DriverPersonalIdentificationCode))
                .Returns(driver);

            var driverFullName = service.UnsafeGetFullName(trafficTicketId);

            driverFullName.Should().Be($"{driver.FirstName} {driver.LastName}");
        }
    }
}
